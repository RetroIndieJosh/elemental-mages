using UnityEngine;
using System.Collections;

public class SnapToGround : MonoBehaviour
{
    [SerializeField] private float a_yOffset = 0.5f;

    void Update() {
        RaycastHit hit = new RaycastHit();
        if ( Physics.Raycast( transform.position, -transform.up, out hit, Mathf.Infinity ) ) {
            transform.position = hit.point + Vector3.up * a_yOffset;
            enabled = false;
        }
    }
}