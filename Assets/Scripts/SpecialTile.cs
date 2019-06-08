using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    Fire,
    Tree,
    Water
}

[CreateAssetMenu]
public class SpecialTile : Tile
{
    [SerializeField] private TileType m_tileType = TileType.Water;

    public TileType TileType {  get { return m_tileType; } }

    public override void GetTileData( Vector3Int position, ITilemap tilemap, ref TileData tileData ) {
        base.GetTileData( position, tilemap, ref tileData );
    }

    private GameObject MakeObject() {
        var go = new GameObject();

        var boxCollider = go.AddComponent<BoxCollider>();
        boxCollider.size = Vector3.one;
        boxCollider.center = Vector3.up * 0.5f;

        if( m_tileType == TileType.Water ) {
            // TODO
        }

        return go;
    }
}
