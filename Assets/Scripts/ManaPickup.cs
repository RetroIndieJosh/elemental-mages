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

        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if( spriteRenderer != null ) spriteRenderer.enabled = false;

        var meshRenderer = GetComponentInChildren<MeshRenderer>();
        if( meshRenderer != null ) meshRenderer.enabled = false;
        m_taken = true;
    }

    public override void Burn() {
        Destroy( gameObject );
    }

    public override void Wet() { }

    private void Update() {
        var speedMin = 50f;
        var speedMax = 150f;
        var rotX = Random.Range( speedMin, speedMax ) * Time.deltaTime;
        var rotY = Random.Range( speedMin, speedMax ) * Time.deltaTime;
        var rotZ = Random.Range( speedMin, speedMax ) * Time.deltaTime;

        transform.Rotate( rotX, rotY, rotZ );
    }
}
