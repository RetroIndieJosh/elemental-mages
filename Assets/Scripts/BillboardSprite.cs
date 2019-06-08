using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSprite : MonoBehaviour {
    private enum BillboardStyle
    {
        AlwaysFaceCamera,
        MatchDirection
    }

    [SerializeField]
    private BillboardStyle m_style = BillboardStyle.MatchDirection;

    //private SpriteAnimator m_animator = null;

    private void Awake() {
        //m_animator = GetComponent<SpriteAnimator>();
    }

    private void LateUpdate() {
        UpdateRotation();
        //UpdateAnimation();
    }

    /*
    private void UpdateAnimation() {
        if ( m_animator == null ) return;

        if ( m_animator.CurAnimation.Name.StartsWith( "move" ) == false ) return;

        var angle = Vector3.SignedAngle( Camera.main.transform.forward, transform.parent.forward, Vector3.up );
        if ( Mathf.Abs( angle ) < 45.0f ) {
            m_animator.SetAnimation( "move forward" );
        } else if ( Mathf.Abs( angle ) < 135.0f ) {
            m_animator.SetAnimation( "move side" );
            //Debug.Log( "Angle: " + angle );
            m_animator.FlipX = angle > 0;
        } else m_animator.SetAnimation( "move back" );
    }
    */

    private void UpdateRotation() {
        if( m_style == BillboardStyle.AlwaysFaceCamera)
            transform.LookAt( Camera.main.transform, Vector3.up );
        else transform.forward = Camera.main.transform.forward;
    }

}
