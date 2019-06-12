using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// OBSOLETE
//

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject m_targetFocus = null;
    [SerializeField] private float m_distanceBack = 2f;
    [SerializeField] private float m_distanceUp = 2f;

    [Header( "Movement" )]
    [SerializeField] private float m_smoothTime = 0.3f;

    public GameObject TargetFocus {
        set {
            m_targetFocus = value;
            m_focusPosition = m_targetFocus.transform.position;
        }
    }

    private Vector3 m_focusPosition = Vector3.zero;
    private Vector3 m_velocity = Vector3.zero;

    private void Start() {
        TargetFocus = m_targetFocus;
    }

    void Update() {
        var targetPos = m_targetFocus.transform.position
            + Vector3.up * m_distanceUp
            + Vector3.back * m_distanceBack;
        transform.position = Vector3.SmoothDamp( transform.position, targetPos, ref m_velocity, m_smoothTime );
    }
}
