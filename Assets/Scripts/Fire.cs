using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireState
{
    Burning,
    Out
}

public class Fire : TileComponent3d
{
    public FireState FireState { get; private set; }

    public override void Burn() { }

    public override void Wet() {
        FireState = FireState.Out;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponentInChildren<Collider>().enabled = false;
    }

    private void Awake() {
        FireState = FireState.Burning;
    }
}
