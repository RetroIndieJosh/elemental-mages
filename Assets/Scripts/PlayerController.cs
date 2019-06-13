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


[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, 
    MainControls.IMovementActions, MainControls.IPlayerSwitchActions, MainControls.ICastActions
{
    static public PlayerController activePlayer = null;

    static public int ActiveMageCount { get { return ActiveMageList.Count; } }

    static public List<PlayerController> ActiveMageList {
        get {
            return FindObjectsOfType<PlayerController>().ToList();
        }
    }

    static public void ClearMageList() { s_mageList.Clear(); }

    static private List<PlayerController> s_mageList = new List<PlayerController>();

    static public void ActivateMage( PlayerType a_type, Vector3 a_position ) {
        var mage = GetMage( a_type );
        mage.gameObject.SetActive( true );
        mage.transform.position = a_position;
    }

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

    [SerializeField] private PlayerType m_playerType = PlayerType.Fire;
    [SerializeField] private float m_moveSpeed = 5f;

    [Header( "Casting" )]
    [SerializeField] private ParticleSystem m_spellParticles = null;
    [SerializeField] private float m_spellCooldownTimeSec = 0f;
    [SerializeField] private AudioClip m_spellCastSound = null;

    public PlayerType PlayerType {  get { return m_playerType; } }
    public string ColorString {
        get {
            switch ( PlayerType ) {
                case PlayerType.Air: return "yellow";
                case PlayerType.Earth: return "gree";
                case PlayerType.Fire: return "red";
                case PlayerType.Water: return "blue";
                default: return "white";
            }
        }
    }

    private bool m_flipZ = false;
    private Rigidbody m_body = null;
    private MainControls m_mainControls = null;
    private float m_timeSinceLastCastSec = Mathf.Infinity;
    private Vector2 m_castDirection = Vector2.zero;
    private Animator m_animator = null;

    private RigidbodyConstraints m_initialConstraints = RigidbodyConstraints.None;

    private void Awake() {
        m_body = GetComponent<Rigidbody>();
        m_initialConstraints = m_body.constraints;
        s_mageList.Add( this );
        m_animator = GetComponentInChildren<Animator>();
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
    }

    private void FixedUpdate() {
        var yRot = CameraController.instance.Rotation;
        var move3d = new Vector3( m_move.x, 0f, m_move.y );
        var angledMove = Quaternion.AngleAxis( yRot, Vector3.up ) * move3d;
        m_body.velocity = new Vector3( angledMove.x, 0f, angledMove.z );
    }

    private void Start() {
        m_animator.SetFloat( "animSpeed", WorldGenerator.instance.MageAnimateSpeed );
    }

    private void Update() {
        m_timeSinceLastCastSec += Time.deltaTime;

        if ( m_timeSinceLastCastSec >= m_spellCooldownTimeSec )
            WorldGenerator.instance.MageInfo += " ***";
    }

    IEnumerator StopCastAnimation() {
        yield return new WaitForSeconds( m_spellParticles.main.startLifetime.constant );
    }

    public void OnCast(InputAction.CallbackContext context ) {
        if ( m_timeSinceLastCastSec < m_spellCooldownTimeSec ) return;
        if ( context.performed == false ) return;
        if ( WorldGenerator.instance.CanCast == false ) return;

        //Debug.Log( "Cast spell" );

        AudioSource.PlayClipAtPoint( m_spellCastSound, transform.position );

        var yRot = CameraController.instance.Rotation;
        //m_spellParticles.transform.forward = new Vector3( m_stickInput.x, 0f, m_stickInput.y );
        m_spellParticles.transform.forward = Quaternion.AngleAxis( yRot, Vector3.up )
            * new Vector3( m_castDirection.x, 0f, m_castDirection.y );

        m_spellParticles.Play();
        m_timeSinceLastCastSec = 0f;
        WorldGenerator.instance.UseMana();
        if( m_flipZ ) m_animator.Play( "Cast Back" );
        else m_animator.Play( "Cast" );

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

    private Vector2 m_move = Vector2.zero;

    public void OnMove(InputAction.CallbackContext context) {
        m_move = context.ReadValue<Vector2>() * m_moveSpeed;
        Debug.Log( "Move: " + m_move );


        if ( m_move.magnitude < Mathf.Epsilon ) {
            m_move = Vector2.zero;

            if ( m_flipZ ) m_animator.Play( "Idle Back" );
            else m_animator.Play( "Idle" );

            return;
        } 

        // remember our last move input for casting direction
        m_castDirection = m_move.normalized;

        m_flipZ = m_move.y > Mathf.Epsilon;

        if ( m_flipZ ) m_animator.Play( "Walk Back" );
        else m_animator.Play( "Walk" );

        GetComponentInChildren<SpriteRenderer>().flipX = m_move.x < -Mathf.Epsilon;
    }

    public void OnNextPlayer( InputAction.CallbackContext context ) {
        ControlPlayer( -1 );
    }

    public void OnRestart( InputAction.CallbackContext context ) {
        WorldGenerator.instance.StartLevel();
    }

    public void OnPrevPlayer( InputAction.CallbackContext context ) {
        ControlPlayer( 1 );
    }

    static public void ControlPlayer( PlayerController a_player ) {
        foreach ( var player in ActiveMageList )
            player.enabled = false;

        // deactivate previous
        if ( activePlayer != null ) {
            activePlayer.m_move = Vector2.zero;
            activePlayer.m_body.velocity = Vector3.zero;
            activePlayer.enabled = false;
        }

        // enable new
        activePlayer = a_player;
        activePlayer.enabled = true;

        //Debug.Log( $"New player: {activePlayer.name}" );
    }

    static public void ControlPlayer(int a_indexChange ) {
        if ( a_indexChange == 0 ) return;

        if( activePlayer == null ) {
            Debug.LogError( "Tried to change active player with relative index but no active player" );
            return;
        }

        var list = ActiveMageList;

        var index = ( list.IndexOf( activePlayer ) + a_indexChange ) % list.Count;
        if ( index < 0 ) index += list.Count;
        ControlPlayer( list[index] );
    }
}
