using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Wall))]
public class Wall : MonoBehaviour
{
    private Material m_material = null;

    private void Awake() {
        m_material = GetComponent<MeshRenderer>().material;
    }

    void Update() {
        if ( PlayerController.activePlayer == null ) return;

        m_material.color = Color.white;

        var camPos = Camera.main.transform.position;
        var playerPos = PlayerController.activePlayer.transform.position;
        var direction = ( playerPos - camPos ).normalized;

        if ( Physics.Raycast( camPos, direction, out RaycastHit hitInfo ) == false )
            return;

        if ( hitInfo.collider.gameObject == gameObject ) {
            var color = m_material.color;
            m_material.color = new Color( color.r, color.g, color.b, 0.5f );
        }
    }
}
