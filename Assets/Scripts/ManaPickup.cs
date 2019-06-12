using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : TileComponent3d
{
    [SerializeField] private int m_manaCount = 0;

    private bool m_taken = false;

    private void OnTriggerEnter( Collider a_collider ) {
        if ( m_taken ) return;
        if ( a_collider.GetComponent<PlayerController>() == null ) 
            return;

        WorldGenerator.instance.AddMana( m_manaCount );
        GetComponentInChildren<Collider>().enabled = false;
        //GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponentInChildren<MeshRenderer>().enabled = false;
        m_taken = true;
    }

    public override void Burn() {
        Destroy( gameObject );
    }

    public override void Wet() { }

    private void Update() {
        transform.Rotate( Vector3.one, -100f * Time.deltaTime );
    }
}
