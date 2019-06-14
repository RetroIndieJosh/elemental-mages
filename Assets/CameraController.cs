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
    [SerializeField] private float m_overheadHeight = 10f;

    [SerializeField] private GameObject m_helpDisplay = null;

    public bool IsOverhead { get; private set; }
    public float Rotation { get { return transform.eulerAngles.y; } }
    public float TargetRotation { get; private set; }

    private MainControls m_mainControls = null;
    private Vector3 m_rotateVelocity = Vector3.zero;
    private Vector3 m_velocity = Vector3.zero;

    public void OnHelp( InputAction.CallbackContext context ) {
        if ( context.performed == false ) return;

        if ( m_helpDisplay == null ) return;
        m_helpDisplay.SetActive( !m_helpDisplay.activeSelf );
    }

    public void OnOverhead( InputAction.CallbackContext context ) {
        if ( context.performed == false ) return;
        IsOverhead = !IsOverhead;
        TargetRotation = 0f;
    }

    public void OnRotate( InputAction.CallbackContext context ) {
        if ( IsOverhead ) return;

        var move = context.ReadValue<float>();
        //transform.parent.Rotate( Vector3.up, move );
        TargetRotation += move;
    }

    public void ResetRotation() {
        TargetRotation = 0f;
    }

    private void Awake() {
        instance = this;
        IsOverhead = false;
        TargetRotation = 0f;

        m_mainControls = new MainControls();
        m_mainControls.Camera.SetCallbacks( this );
        m_mainControls.Enable();

        m_helpDisplay.SetActive( false );

        transform.position = Vector3.zero;
    }

    private void LateUpdate() {
        if ( PlayerController.activePlayer == null ) return;

        var targetLookPos = IsOverhead ?
            Vector3.up * ( m_overheadHeight - 1f ) :
            PlayerController.activePlayer.transform.position;

        var rotation = Quaternion.AngleAxis( TargetRotation, Vector3.up );
        var targetMovePos = IsOverhead ?
            new Vector3( 0f, m_overheadHeight, 0f ) :
            targetLookPos + rotation * m_distance;
        transform.position = Vector3.SmoothDamp( transform.position, targetMovePos, ref m_velocity, m_dampSpeed );

        transform.LookAt( targetLookPos );
    }
}
