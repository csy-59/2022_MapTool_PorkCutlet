using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TileType = MapToolSelecter.ETileType;

public class TileMaster : MonoBehaviour
{
    [SerializeField] private GameObject[] _blocks;
    private TileType _currentTileType = TileType.Basic;
    public int TileNumber { get; set; }

    public void SelectTile(TileType _type)
    {
        _blocks[(int)_currentTileType].SetActive(false);
        _currentTileType = _type;
        _blocks[(int)_currentTileType].SetActive(true);
    }
}
