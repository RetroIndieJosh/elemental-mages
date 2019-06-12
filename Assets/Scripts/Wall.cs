using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[DisallowMultipleComponent]
[RequireComponent(typeof(Wall))]
public class Wall : MonoBehaviour
{
    private Tilemap m_tileMap = null;

    private void Awake() {
        m_tileMap = GetComponent<Tilemap>();
    }

    void Update() {
        if ( PlayerController.activePlayer == null ) return;

        m_tileMap.color = Color.white;

        var camPos = Camera.main.transform.position;
        var playerPos = PlayerController.activePlayer.transform.position;
        var direction = ( playerPos - camPos ).normalized;

        if ( Physics.Raycast( camPos, direction, out RaycastHit hitInfo ) == false )
            return;

        if ( hitInfo.collider.gameObject == gameObject ) {
            var color = m_tileMap.color;
            m_tileMap.color = new Color( color.r, color.g, color.b, 0.5f );
        }
    }
}
