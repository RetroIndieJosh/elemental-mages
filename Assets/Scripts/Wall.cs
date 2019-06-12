using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

[DisallowMultipleComponent]
[RequireComponent(typeof(Wall))]
public class Wall : MonoBehaviour
{
    private Tilemap m_tileMap = null;

    private void Awake() {
        m_tileMap = GetComponent<Tilemap>();
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere( Origin, 0.2f );
        Gizmos.DrawLine( Origin, TowardPlayer * 100 );
    }

    private Vector3 TowardPlayer {
        get {
            if ( Camera.main == null || PlayerController.activePlayer == null )
                return Vector3.zero;

            var camPos = Camera.main.transform.position;
            var playerPos = PlayerController.activePlayer.transform.position;
            return ( playerPos - camPos ).normalized;
        }
    }

    private Vector3 Origin {
        get {
            return Camera.main.transform.position - TowardPlayer;
        }
    }

    void Update() {
        if ( PlayerController.activePlayer == null ) return;

        m_tileMap.color = Color.white;

        var hitInfos = Physics.SphereCastAll( Origin, 0.2f, TowardPlayer );

        foreach( var hitInfo in hitInfos.Reverse() ) {
            if ( hitInfo.collider.gameObject != gameObject )
                continue;

            if ( hitInfo.collider.GetComponent<PlayerController>() != null )
                return;

            var color = m_tileMap.color;
            m_tileMap.color = new Color( color.r, color.g, color.b, 0.5f );
        }
    }
}
