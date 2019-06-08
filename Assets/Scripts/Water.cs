using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileComponent3d: MonoBehaviour
{
    public Vector3Int tilePos = Vector3Int.zero;
}

public class Water : TileComponent3d
{
    [SerializeField] private TileBase iceTile = null;
    [SerializeField] private TileBase waterTile = null;

    private Collider m_collider = null;
    private bool m_isIce = false;

    private void OnTriggerEnter( Collider other ) {
        WorldGenerator.instance.TileMap.SetTile( tilePos, iceTile );
        m_isIce = true;
    }

    private void OnTriggerExit( Collider other ) {
        if ( PlayerController.activePlayer.PlayerType != PlayerType.Fire )
            return;

        WorldGenerator.instance.TileMap.SetTile( tilePos, waterTile );
        m_isIce = false;
    }

    private void Awake() {
        m_collider = GetComponent<Collider>();
    }

    private void Update() {
        m_collider.isTrigger = m_isIce || ( PlayerController.activePlayer.PlayerType == PlayerType.Water );
    }
}
