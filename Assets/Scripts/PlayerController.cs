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


public enum Facing
{
    East,
    North,
    South,
    West
}


[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, 
    MainControls.IMovementActions, MainControls.IPlayerSwitchActions, MainControls.ICastActions
{
    static public PlayerController activePlayer = null;

    [SerializeField] private bool m_isInitialPlayer = false;
    [SerializeField] private PlayerType m_playerType = PlayerType.Fire;
    [SerializeField] private float m_moveSpeed = 5f;

    public PlayerType PlayerType {  get { return m_playerType; } }

    private Facing m_facing = Facing.East;
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
            m_mainControls.Cast.SetCallbacks( this );
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

    [SerializeField] ParticleSystem m_spellParticles = null;

    private float FacingAngle {
        get {
            switch( m_facing ) {
                case Facing.East: return 270f;
                case Facing.North: return 0f;
                case Facing.South: return 180f;
                case Facing.West: return 90f;
                default: return 0f;
            }
        }
    }

    public void OnCast(InputAction.CallbackContext context ) {
        if ( context.performed == false ) return;

        Debug.Log( "Cast spell" );

        //m_spellParticles.transform.Rotate( Vector3.up, FacingAngle );
        m_spellParticles.transform.forward = new Vector3( m_stickInput.x, 0f, m_stickInput.y );
        m_spellParticles.Play();

        if ( Physics.Raycast( transform.position, m_spellParticles.transform.forward, out RaycastHit hit, 1f  ) ) {
            Debug.Log( "Hit something" );
            var tree = hit.collider.GetComponentInChildren<Plant>();
            if ( tree == null ) return;

            Debug.Log( "Hit a tree" );
            tree.Burn();
        }
    }

    private Vector2 m_stickInput = Vector2.zero;

    public void OnMove(InputAction.CallbackContext context) {
        var move = context.ReadValue<Vector2>() * m_moveSpeed;
        if ( move.magnitude < Mathf.Epsilon ) {
            m_body.velocity = Vector3.zero;
            return;
        } 

        // remember our last move input for casting direction
        m_stickInput = move;

        m_body.velocity = new Vector3( move.x, 0f, move.y );

        var prevFacing = m_facing;

        if ( move.y > Mathf.Epsilon ) m_facing = Facing.North;
        else if ( move.y < -Mathf.Epsilon ) m_facing = Facing.South;
        else if ( move.x > Mathf.Epsilon ) m_facing = Facing.East;
        else m_facing = Facing.West;

        var wasFacingWest = prevFacing == Facing.West && m_facing != Facing.East;
        GetComponentInChildren<SpriteRenderer>().flipX = m_facing == Facing.West || wasFacingWest;

        //Debug.Log( $"Facing {m_facing}" );
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

        //Debug.Log( $"New player: {activePlayer.name}" );
    }
}
