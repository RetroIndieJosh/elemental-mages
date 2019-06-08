using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TreeState
{
    Normal,
    Burning
}

public class Tree : TileComponent3d
{
    [SerializeField] private Sprite m_normalSprite = null;
    [SerializeField] private Sprite m_burningSprite = null;
    [SerializeField] private Sprite m_burntTree = null;

    public void Burn() {
        GetComponent<SpriteRenderer>().sprite = m_burningSprite;
    }

    private void Start() {
        GetComponent<SpriteRenderer>().sprite = m_normalSprite;
    }
}
