using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.Tilemaps;

public enum PlayerType
{
    Air,
    Earth,
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

    [Header("Casting")]
    [SerializeField] private ParticleSystem m_spellParticles = null;
    [SerializeField] private float m_spellCooldownTimeSec = 0f;
    [SerializeField] private AudioClip m_spellCastSound = null;

    public PlayerType PlayerType {  get { return m_playerType; } }

    static private List<PlayerController> s_mageList = new List<PlayerController>();

    static public void DisableAll() {
        var mageList = FindObjectsOfType<PlayerController>();
        foreach ( var mage in mageList )
            mage.gameObject.SetActive( false );
    }

    static public PlayerController GetMage(PlayerType a_type ) {
        foreach ( var mage in s_mageList )
            if ( mage.PlayerType == a_type )
                return mage;
        return null;
    }

    static public void ActivateMage( PlayerType a_type, Vector3 a_position ) {
        var mage = GetMage( a_type );
        mage.gameObject.SetActive( true );
        mage.transform.position = a_position;
    }

    private Facing m_facing = Facing.East;
    private Rigidbody m_body = null;
    private MainControls m_mainControls = null;
    private float m_timeSinceLastCastSec = 0f;
    private Vector2 m_stickInput = Vector2.zero;

    private RigidbodyConstraints m_initialConstraints = RigidbodyConstraints.None;

    private void Awake() {
        m_body = GetComponent<Rigidbody>();
        m_initialConstraints = m_body.constraints;
        s_mageList.Add( this );
    }

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

    private void Update() {
        m_timeSinceLastCastSec += Time.deltaTime;
    }

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
        if ( m_timeSinceLastCastSec < m_spellCooldownTimeSec ) return;
        if ( context.performed == false ) return;

        //Debug.Log( "Cast spell" );

        AudioSource.PlayClipAtPoint( m_spellCastSound, transform.position );
        m_spellParticles.transform.forward = new Vector3( m_stickInput.x, 0f, m_stickInput.y );
        m_spellParticles.Play();
        m_timeSinceLastCastSec = 0f;

        if ( Physics.Raycast( transform.position, m_spellParticles.transform.forward, out RaycastHit hit, 1f  ) ) {
            //Debug.Log( "Hit something" );
            var target = hit.collider.GetComponentInChildren<TileComponent3d>();
            if ( target == null ) return;

            //Debug.Log( "Hit a tree" );
            if( PlayerType == PlayerType.Fire )
                target.Burn();
            else if( PlayerType == PlayerType.Water )
                target.Wet();
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        var move = context.ReadValue<Vector2>();
        var move3d = new Vector3( move.x, 0f, move.y );

        var cameraFocus = Camera.main.transform.parent;
        if ( cameraFocus == null ) return;

        var yRot = cameraFocus.rotation.eulerAngles.y;
        move3d = Quaternion.AngleAxis( yRot, Vector3.up ) * move3d;
        move = new Vector2( move3d.x, move3d.z );

        move *= m_moveSpeed;
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
