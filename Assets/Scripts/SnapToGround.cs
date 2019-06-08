using UnityEngine;
using System.Collections;

public class SnapToGround : MonoBehaviour
{
    [SerializeField] private float m_yOffset = 0.5f;
    [SerializeField] private bool m_snapOnce = true;
    [SerializeField] private bool m_lockY = false;

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
            if( m_lockY )
                GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;
            if ( m_snapOnce )
                enabled = false;
        }
    }
}