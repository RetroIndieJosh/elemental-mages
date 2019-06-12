using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    Exit,
    Fire,
    Tree,
    Water,
    StartAir,
    StartEarth,
    StartFire,
    StartWater,
    ManaPickup
}

[CreateAssetMenu]
public class SpecialTile : Tile
{
    [SerializeField] private TileType m_tileType = TileType.Water;

    public TileType TileType {  get { return m_tileType; } }

    public override void GetTileData( Vector3Int position, ITilemap tilemap, ref TileData tileData ) {
        base.GetTileData( position, tilemap, ref tileData );
    }
}
