using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    static public WorldGenerator instance = null;

    [SerializeField] private Tilemap m_tileMap = null;

    [Header("Prefabs")]
    [SerializeField] private GameObject m_treePrefab = null;
    [SerializeField] private GameObject m_waterPrefab = null;

    [Header( "Game Balance" )]
    [SerializeField] private float m_firePropogationTimeSec = 0.5f;

    public Tilemap TileMap {  get { return m_tileMap; } }

    private GameObject[,] m_tileObjects = null;

    private TileComponent3d GetTileComponent3D( Vector2Int a_tilePos ) {
        var tilePos = new Vector3Int( a_tilePos.x, a_tilePos.y, 0 );
        var tile = m_tileMap.GetTile( tilePos ) as SpecialTile;
        if ( tile == null ) return null;

        var ix = tilePos.x - m_tileMap.origin.x;
        var iy = tilePos.y - m_tileMap.origin.y;
        if ( ( ix < 0 || ix >= m_tileMap.size.x ) || ( iy < 0 || iy >= m_tileMap.size.y ) ) {
            Debug.LogError( "Tile index out of range" );
            return null;
        }

        var tileObj = m_tileObjects[ix, iy];
        if ( tileObj == null ) return null;

        TileComponent3d tileComp = null;
        if ( tile.TileType == TileType.Tree ) tileComp = tileObj.GetComponentInChildren<Plant>();
        else if ( tile.TileType == TileType.Water ) tileComp = tileObj.GetComponentInChildren<Water>();

        return tileComp;
    }

    private List<TileComponent3d> GetAdjacentTiles(Vector2Int a_centerTilePos ) {
        var list = new List<TileComponent3d>();

        for ( var x = -1; x <= 1; ++x ) {
            if ( x == 0 ) continue;
            var tilePos = a_centerTilePos;
            tilePos.x += x;
            var tileComp = GetTileComponent3D( tilePos );
            if ( tileComp != null ) list.Add( tileComp );
        }

        for ( var y = -1; y <= 1; ++y ) {
            if ( y == 0 ) continue;
            var tilePos = a_centerTilePos;
            tilePos.y += y;
            var tileComp = GetTileComponent3D( tilePos );
            if ( tileComp != null ) list.Add( tileComp );
        }

        return list;
    }

    private void Awake() {
        instance = this;
    }

    private void Start() {
        m_tileObjects = new GameObject[m_tileMap.size.x, m_tileMap.size.y];
    }

    void Update() {
        UpdateTileObjects();
        UpdateFirePropogation();
    }

    private void UpdateFirePropogation() {
        for( var x = 0; x < m_tileMap.size.x; ++x ) {
            for ( var y = 0; y < m_tileMap.size.y; ++y ) {
                var tileObj = m_tileObjects[x, y];
                if ( tileObj == null ) continue;

                var centerTree = tileObj.GetComponentInChildren<Plant>();
                if ( centerTree == null ) continue;

                if ( centerTree.PlantState != PlantState.Burning || centerTree.BurnTime < m_firePropogationTimeSec )
                    continue;

                var absolutePos = new Vector2Int( x + m_tileMap.origin.x, y + m_tileMap.origin.y );
                var adjacentTiles = GetAdjacentTiles( absolutePos );
                foreach( var tile in adjacentTiles ) {
                    var tree = tile as Plant;
                    if ( tree == null ) continue;
                    tree.Burn();
                }
            }
        }
    }

    private void UpdateTileObjects() {
        var startPos = m_tileMap.origin;
        var endPos = m_tileMap.origin + m_tileMap.size;
        var tilePos = startPos;
        for ( tilePos.x = startPos.x; tilePos.x <= endPos.x; ++tilePos.x ) {
            for ( tilePos.y = startPos.y; tilePos.y <= endPos.y; ++tilePos.y ) {
                var tile = m_tileMap.GetTile( tilePos ) as SpecialTile;
                if ( tile == null )
                    continue;

                GameObject go = null;

                if ( tile.TileType == TileType.Tree ) {
                    go = CreateTileObject<Plant>( tilePos, m_treePrefab );
                } else if ( tile.TileType == TileType.Water ) {
                    go = CreateTileObject<Water>( tilePos, m_waterPrefab );
                }

                if ( go == null ) continue;

                var ix = tilePos.x - m_tileMap.origin.x;
                var iy = tilePos.y - m_tileMap.origin.y;
                m_tileObjects[ix, iy] = go;
                Debug.Log( $"New {tile.TileType} at {tilePos} ({ix}, {iy})" );
            }
        }
    }

    private bool CheckChanged<T>( Vector3Int a_tilePos ) where T: TileComponent3d {
        var ix = a_tilePos.x - m_tileMap.origin.x;
        var iy = a_tilePos.y - m_tileMap.origin.y;
        var obj = m_tileObjects[ix, iy];
        if ( obj == null ) return true;

        return obj.GetComponentInChildren<T>() == null;
    }

    private GameObject CreateTileObject<T>( Vector3Int a_tilePos, GameObject a_prefab ) where T: TileComponent3d {
        if ( CheckChanged<T>( a_tilePos ) == false ) return null;

        var worldPos = m_tileMap.CellToWorld( a_tilePos ) + new Vector3( 0.5f, 0.0f, -0.5f );
        var go = Instantiate( a_prefab, worldPos, Quaternion.identity );
        var comp = go.GetComponentInChildren<T>();
        comp.tilePos = a_tilePos;

        return go;
    }
}
