using UnityEngine;
using System.Collections;

public class SnapToGround : MonoBehaviour
{
    [SerializeField] private float m_yOffset = 0.5f;

    private void Start() {
        TrySnap();
    }

    void Update() {
        TrySnap();
    }

    private void TrySnap() {
        Debug.Log( $"Try snap {name}" );
        RaycastHit hit = new RaycastHit();
        if ( Physics.Raycast( transform.position, -transform.up, out hit, Mathf.Infinity ) ) {
            transform.position = hit.point + Vector3.up * m_yOffset;
            enabled = false;
        }
    }
}