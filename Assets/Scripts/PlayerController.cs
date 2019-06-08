using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, MainControls.IMovementActions, MainControls.IPlayerSwitchActions
{
    [SerializeField] private bool m_isInitialPlayer = false;
    [SerializeField] private float m_moveSpeed = 5f;

    private Rigidbody m_body = null;
    private MainControls m_mainControls = null;

    private void Awake() {
        m_body = GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        if( Keyboard.current == null ) {
            Debug.LogError( "No keyboard found, destroying controller." );
            Destroy( this );
            return;
        }

        if ( m_mainControls == null ) {
            m_mainControls = new MainControls();
            m_mainControls.Movement.SetCallbacks( this );
            m_mainControls.PlayerSwitch.SetCallbacks( this );
        }
        m_mainControls.Enable();
    }

    private void OnDisable() {
        m_mainControls.Disable();
    }

    private void Start() {
        enabled = m_isInitialPlayer;
    }

    public void OnMove(InputAction.CallbackContext context) {
        var move = context.ReadValue<Vector2>() * m_moveSpeed;
        m_body.velocity = new Vector3( move.x, 0f, move.y );
    }

    public void OnNextPlayer( InputAction.CallbackContext context ) {
        ActivatePlayer( -1 );
    }

    public void OnPrevPlayer( InputAction.CallbackContext context ) {
        ActivatePlayer( 1 );
    }

    private void ActivatePlayer(int a_indexChange ) {
        var list = FindObjectsOfType<PlayerController>().ToList();
        var index = ( list.IndexOf( this ) + a_indexChange ) % list.Count;
        if ( index < 0 ) index += list.Count;

        m_body.velocity = Vector3.zero;

        enabled = false;
        list[index].enabled = true;

        Debug.Log( $"New player: {index}" );
    }
}
