using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    static public WorldGenerator instance = null;

    [SerializeField] private Tilemap m_tileMap = null;

    [Header( "Levels" )]
    [SerializeField] private int m_startLevelIndex = 0;
    [SerializeField] private List<Tilemap> m_levelTileMaps = new List<Tilemap>();

    [Header("Prefabs")]
    [SerializeField] private GameObject m_exitPrefab = null;
    [SerializeField] private GameObject m_firePrefab = null;
    [SerializeField] private GameObject m_treePrefab = null;
    [SerializeField] private GameObject m_waterPrefab = null;

    [Header( "Game Balance" )]
    [SerializeField] private float m_firePropogationTimeSec = 0.5f;

    [SerializeField, Tooltip("In addition to propogation time") ]
    private float m_fireBurnDownTimeSec = 0.5f;

    public Tilemap TileMap {  get { return m_tileMap; } }

    public float FireBurnDownTimeSecTotal { get { return m_firePropogationTimeSec + m_fireBurnDownTimeSec; } }

    private GameObject[,] m_tileObjects = null;
    private int m_levelIndex = 0;

    public void NextLevel() {
        Debug.Log( "Level complete!" );
        ++m_levelIndex;
        StartLevel();
        // TODO
    }

    private void StartLevel() {
        for ( var x = 0; x < m_tileMap.size.x; ++x ) {
            for ( var y = 0; y < m_tileMap.size.y; ++y ) {
                if ( m_tileObjects[x, y] == null ) continue;

                Destroy( m_tileObjects[x, y].gameObject );
                m_tileObjects[x, y] = null;
            }
        }

        m_tileMap.gameObject.SetActive( false );
        if ( m_levelIndex >= m_levelTileMaps.Count ) {
            m_tileMap = null;
            return;
        }

        m_tileMap = m_levelTileMaps[m_levelIndex];
        m_tileMap.gameObject.SetActive( true );
    }

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
        if ( tile.TileType == TileType.Exit ) tileComp = tileObj.GetComponentInChildren<Exit>();
        else if ( tile.TileType == TileType.Fire ) tileComp = tileObj.GetComponentInChildren<Fire>();
        else if ( tile.TileType == TileType.Tree ) tileComp = tileObj.GetComponentInChildren<Plant>();
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
        foreach ( var tileMap in m_levelTileMaps )
            tileMap.gameObject.SetActive( false );

        m_tileObjects = new GameObject[m_tileMap.size.x, m_tileMap.size.y];
        m_levelIndex = m_startLevelIndex;
        StartLevel();
    }

    void Update() {
        if ( m_tileMap == null ) return;

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

                if ( tile.TileType == TileType.Exit ) {
                    go = CreateTileObject<Exit>( tilePos, m_exitPrefab );
                } else if ( tile.TileType == TileType.Fire ) {
                    go = CreateTileObject<Fire>( tilePos, m_firePrefab );
                } else if ( tile.TileType == TileType.Tree ) {
                    go = CreateTileObject<Plant>( tilePos, m_treePrefab );
                } else if ( tile.TileType == TileType.Water ) {
                    go = CreateTileObject<Water>( tilePos, m_waterPrefab );
                }

                if ( go == null ) continue;

                var ix = tilePos.x - m_tileMap.origin.x;
                var iy = tilePos.y - m_tileMap.origin.y;
                m_tileObjects[ix, iy] = go;
                //Debug.Log( $"New {tile.TileType} at {tilePos} ({ix}, {iy})" );
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

        var worldPos = m_tileMap.CellToWorld( a_tilePos ) + new Vector3( 0.5f, 0.5f, -0.5f );
        var go = Instantiate( a_prefab, worldPos, Quaternion.identity );
        var comp = go.GetComponentInChildren<T>();
        comp.tilePos = a_tilePos;

        return go;
    }
}
