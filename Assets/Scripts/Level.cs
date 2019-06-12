using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    [SerializeField] private Tilemap m_map = null;
    [SerializeField] private int m_startMana = 3;
    [SerializeField] private PlayerType m_startPlayerType = PlayerType.Fire;

    public Tilemap Map {  get { return m_map; } }
    public int StartMana { get { return m_startMana; } }
    public PlayerType StartPlayerType {  get { return m_startPlayerType; } }
}
