using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.Tilemaps;

public enum PlayerType
{
    Fire,
    Water
}

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, MainControls.IMovementActions, MainControls.IPlayerSwitchActions
{
    static public PlayerController activePlayer = null;

    [SerializeField] private bool m_isInitialPlayer = false;
    [SerializeField] private PlayerType m_playerType = PlayerType.Fire;
    [SerializeField] private float m_moveSpeed = 5f;

    public PlayerType PlayerType {  get { return m_playerType; } }

    private Rigidbody m_body = null;
    private MainControls m_mainControls = null;

    private void Awake() {
        m_body = GetComponent<Rigidbody>();
        m_initialConstraints = m_body.constraints;
    }

    private RigidbodyConstraints m_initialConstraints = RigidbodyConstraints.None;

    private void OnDisable() {
        m_mainControls.Disable();
        m_body.constraints = RigidbodyConstraints.FreezeAll;
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

        m_body.constraints = m_initialConstraints;
        Camera.main.GetComponent<CameraMover>().TargetFocus = gameObject;
    }

    private void Start() {
        if( m_isInitialPlayer )
            ActivatePlayer( 0 );
    }

    public void OnMove(InputAction.CallbackContext context) {
        var move = context.ReadValue<Vector2>() * m_moveSpeed;
        if ( move.magnitude < Mathf.Epsilon )
            m_body.velocity = Vector3.zero;
        else m_body.velocity = new Vector3( move.x, 0f, move.y );
    }

    public void OnNextPlayer( InputAction.CallbackContext context ) {
        ActivatePlayer( -1 );
    }

    public void OnPrevPlayer( InputAction.CallbackContext context ) {
        ActivatePlayer( 1 );
    }

    private void ActivatePlayer(int a_indexChange ) {
        var list = FindObjectsOfType<PlayerController>().ToList();
        foreach ( var player in list )
            player.enabled = false;

        if ( a_indexChange != 0 ) {
            var index = ( list.IndexOf( this ) + a_indexChange ) % list.Count;
            if ( index < 0 ) index += list.Count;

            m_body.velocity = Vector3.zero;

            enabled = false;
            activePlayer = list[index];
        } else {
            activePlayer = this;
        }

        activePlayer.enabled = true;

        Debug.Log( $"New player: {activePlayer.name}" );
    }
}
