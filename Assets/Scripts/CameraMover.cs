using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject m_targetFocus = null;
    [SerializeField] private float m_distanceBack = 2f;
    [SerializeField] private float m_distanceUp = 2f;

    public GameObject TargetFocus {
        set {
            m_targetFocus = value;
            m_focusPosition = m_targetFocus.transform.position;
        }
    }

    private Vector3 m_focusPosition = Vector3.zero;

    private void Start() {
        TargetFocus = m_targetFocus;
    }

    void Update() {
        transform.position = m_targetFocus.transform.position
            + Vector3.up * m_distanceUp
            + Vector3.back * m_distanceBack;
    }
}
