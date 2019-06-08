using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Water : MonoBehaviour
{
    [SerializeField] private TileBase iceTile = null;

    public Vector3Int tilePos = Vector3Int.zero;

    private Collider m_collider = null;

    private void OnTriggerEnter( Collider other ) {
        WorldGenerator.instance.TileMap.SetTile( tilePos, iceTile );
    }

    private void Awake() {
        m_collider = GetComponent<Collider>();
    }

    private void Update() {
        m_collider.isTrigger = ( PlayerController.activePlayer.PlayerType == PlayerType.Water );
    }
}
