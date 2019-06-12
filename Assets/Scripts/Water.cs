using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TileComponent3d: MonoBehaviour
{
    [HideInInspector]
    public Vector3Int tilePos = Vector3Int.zero;

    public abstract void Burn();
    public abstract void Wet();
}

public class Water : TileComponent3d
{
    [SerializeField] private TileBase iceTile = null;
    [SerializeField] private TileBase waterTile = null;

    private Collider m_collider = null;
    private bool m_isIce = false;

    private void OnTriggerEnter( Collider other ) {
        if ( m_isIce ) return;

        WorldGenerator.instance.TileMap.SetTile( tilePos, iceTile );
        m_isIce = true;

        // freezing water uses mana
        WorldGenerator.instance.UseMana();
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
        bool canFreeze = PlayerController.activePlayer.PlayerType == PlayerType.Water 
            && WorldGenerator.instance.CanCast;
        m_collider.isTrigger = m_isIce || canFreeze;
    }

    public override void Burn() { }
    public override void Wet() { }
}
