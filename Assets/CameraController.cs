using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour, MainControls.ICameraActions
{
    [SerializeField] Vector3 m_distance = Vector3.zero;

    private MainControls m_mainControls = null;

    public void OnRotate( InputAction.CallbackContext context ) {
        var move = context.ReadValue<float>();
        transform.parent.Rotate( Vector3.up, move );
    }

    private void Awake() {
        if( transform.parent == null ) {
            Debug.LogError( $"[CameraController] Parent origin missing; please make {name} a child of a GameObject; " +
                $"destroying" );
            Destroy( this );
            return;
        }

        m_mainControls = new MainControls();
        m_mainControls.Camera.SetCallbacks( this );
        m_mainControls.Enable();
    }

    private void Update() {
        var playerPos = PlayerController.activePlayer.transform.position;
        var y = transform.parent.position.y;
        transform.parent.position = new Vector3( playerPos.x, y, playerPos.z );

        transform.LookAt( playerPos );
    }
}
