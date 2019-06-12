using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour, MainControls.ICameraActions
{
    static public CameraController instance = null;

    [SerializeField] private Vector3 m_distance = Vector3.zero;
    [SerializeField] private float m_moveSpeed = 1f;
    [SerializeField] private float m_dampSpeed = 0.3f;

    public float Rotation { get; private set; }

    private MainControls m_mainControls = null;

    public void OnRotate( InputAction.CallbackContext context ) {
        var move = context.ReadValue<float>();
        //transform.parent.Rotate( Vector3.up, move );
        Rotation += move;
    }

    private void Awake() {
        instance = this;
        Rotation = 0f;

        m_mainControls = new MainControls();
        m_mainControls.Camera.SetCallbacks( this );
        m_mainControls.Enable();
    }

    Vector3 m_velocity = Vector3.zero;
    Vector3 m_rotateVelocity = Vector3.zero;

    private void LateUpdate() {
        if ( PlayerController.activePlayer == null ) return;

        var targetLookPos = PlayerController.activePlayer.transform.position;

        var rotation = Quaternion.AngleAxis( Rotation, Vector3.up );
        var targetMovePos = targetLookPos + rotation * m_distance;

        transform.position = Vector3.SmoothDamp( transform.position, targetMovePos, ref m_velocity, m_dampSpeed );

        transform.LookAt( targetLookPos );
    }
}
