using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public enum PlantState
{
    Normal,
    Burning,
    BurnedDown
}

public class Plant : TileComponent3d
{
    [SerializeField] private Sprite m_normalSprite = null;
    [SerializeField] private Sprite m_burningSprite = null;
    [SerializeField] private Sprite m_burntTree = null;

    [Header( "Debug" )]
    [SerializeField] private TextMeshPro m_burnTimeTextMesh = null;

    public PlantState PlantState { get; private set; }

    public float BurnTime { get; private set; }

    private Animator m_animator = null;

    override public void Burn() {
        if ( PlantState != PlantState.Normal ) return;

        m_animator.SetBool( "isBurning", true );
        GetComponent<SpriteRenderer>().sprite = m_burningSprite;
        PlantState = PlantState.Burning;
        BurnTime = 0f;
    }

    override public void Wet() {
        if ( PlantState != PlantState.Burning ) return;

        GetComponent<SpriteRenderer>().sprite = m_burntTree;
        PlantState = PlantState.Normal;
    }

    private void Awake() {
        m_animator = GetComponent<Animator>();
        PlantState = PlantState.Normal;
    }

    private void Update() {
        if ( PlantState != PlantState.Burning ) return;

        BurnTime += Time.deltaTime;
        m_burnTimeTextMesh.text = $"{Mathf.FloorToInt( BurnTime )}";

        if ( BurnTime >= WorldGenerator.instance.FireBurnDownTimeSecTotal ) {
            PlantState = PlantState.BurnedDown;
            GetComponentInParent<Collider>().isTrigger = true;
            GetComponent<SpriteRenderer>().sprite = null;
            m_animator.SetBool( "isBurnedDown", true );
            // TODO modify floor tile?
        }
    }

    private void Start() {
        m_burnTimeTextMesh.text = "";
        GetComponent<SpriteRenderer>().sprite = m_normalSprite;
    }
}
