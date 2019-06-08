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

    private void Awake() {
        instance = this;
    }

    void Start() {
        var startPos = m_tileMap.origin;
        var endPos = m_tileMap.origin + m_tileMap.size;
        var tilePos = startPos;
        for( tilePos.x = startPos.x; tilePos.x <= endPos.x; ++tilePos.x ) {
            for ( tilePos.y = startPos.y; tilePos.y <= endPos.y; ++tilePos.y ) {
                var tile = m_tileMap.GetTile( tilePos ) as SpecialTile;
                if ( tile  == null )
                    continue;
                if( tile.TileType == TileType.Water ) {
                    Debug.Log( "Found a water tile!" );
                    var worldPos = m_tileMap.CellToWorld( tilePos ) + new Vector3( 0.5f, 0.0f, -0.5f );
                    var prefab = Instantiate( m_waterPrefab, worldPos, Quaternion.identity );
                    var water = prefab.GetComponent<Water>();
                    water.tilePos = tilePos;
                }
            }
        }
    }
}
