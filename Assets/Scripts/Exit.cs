using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : TileComponent3d
{
    private void OnTriggerEnter( Collider a_collider ) {
        var player = a_collider.GetComponent<PlayerController>();
        
        if( player == null )
            return;

        if( PlayerController.activePlayer == player ) {
            PlayerController.ControlPlayer( 1 );
        }
        player.gameObject.SetActive( false );
    }

    public override void Burn() { }
    public override void Wet() { }
}
