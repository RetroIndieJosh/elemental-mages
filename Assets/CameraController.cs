﻿using System.Collections;
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

    public bool IsOverhead { get; private set; }
    public float Rotation { get; private set; }

    private MainControls m_mainControls = null;
    private Vector3 m_rotateVelocity = Vector3.zero;
    private Vector3 m_velocity = Vector3.zero;

    public void OnOverhead( InputAction.CallbackContext context ) {
        if ( context.performed == false ) return;
        IsOverhead = !IsOverhead;
        Rotation = 0f;
    }

    public void OnRotate( InputAction.CallbackContext context ) {
        if ( IsOverhead ) return;

        var move = context.ReadValue<float>();
        //transform.parent.Rotate( Vector3.up, move );
        Rotation += move;
    }

    private void Awake() {
        instance = this;
        IsOverhead = false;
        Rotation = 0f;

        m_mainControls = new MainControls();
        m_mainControls.Camera.SetCallbacks( this );
        m_mainControls.Enable();
    }

    private void LateUpdate() {
        if ( PlayerController.activePlayer == null ) return;

        var targetLookPos = IsOverhead ?
            Vector3.zero :
            PlayerController.activePlayer.transform.position;

        var rotation = Quaternion.AngleAxis( Rotation, Vector3.up );
        var targetMovePos = IsOverhead ?
            new Vector3( 0f, m_overheadHeight, 0f ) :
            targetLookPos + rotation * m_distance;
        transform.position = Vector3.SmoothDamp( transform.position, targetMovePos, ref m_velocity, m_dampSpeed );

        transform.LookAt( targetLookPos );
    }
}
