using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : TileComponent3d
{
    private void OnTriggerEnter( Collider a_collider ) {
        if ( a_collider.GetComponent<PlayerController>() != PlayerController.activePlayer )
            return;

        WorldGenerator.instance.NextLevel();
    }

    public override void Burn() { }
    public override void Wet() { }
}
