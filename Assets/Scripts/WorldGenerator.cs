using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    static public WorldGenerator instance = null;

    [SerializeField] private Tilemap m_tileMap = null;
    [SerializeField] private GameObject m_waterPrefab = null;

    public Tilemap TileMap {  get { return m_tileMap; } }

    private GameObject[,] m_tileObjects = null;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        m_tileObjects = new GameObject[m_tileMap.size.x, m_tileMap.size.y];
    }

    void Update() {
        var startPos = m_tileMap.origin;
        var endPos = m_tileMap.origin + m_tileMap.size;
        var tilePos = startPos;
        for ( tilePos.x = startPos.x; tilePos.x <= endPos.x; ++tilePos.x ) {
            for ( tilePos.y = startPos.y; tilePos.y <= endPos.y; ++tilePos.y ) {
                var tile = m_tileMap.GetTile( tilePos ) as SpecialTile;
                if ( tile == null )
                    continue;

                if ( tile.TileType == TileType.Tree )
                    CreateTileObject<Tree>( tilePos );
                else if ( tile.TileType == TileType.Water )
                    CreateTileObject<Water>( tilePos );
            }
        }
    }

    private bool CheckChanged<T>( Vector3Int a_tilePos ) where T: TileComponent3d {
        var ix = a_tilePos.x - m_tileMap.origin.x;
        var iy = a_tilePos.y - m_tileMap.origin.y;
        var obj = m_tileObjects[ix, iy];
        if ( obj == null ) return true;

        return obj.GetComponent<T>() == null;
    }

    private void CreateTileObject<T>(Vector3Int a_tilePos ) where T: TileComponent3d {
        if ( CheckChanged<T>( a_tilePos ) == false ) return;

        var worldPos = m_tileMap.CellToWorld( a_tilePos ) + new Vector3( 0.5f, 0.0f, -0.5f );
        var go = Instantiate( m_waterPrefab, worldPos, Quaternion.identity );
        var comp = go.GetComponent<T>();
        comp.tilePos = a_tilePos;

        var ix = a_tilePos.x - m_tileMap.origin.x;
        var iy = a_tilePos.y - m_tileMap.origin.y;
        m_tileObjects[ix, iy] = go;
    }
}
