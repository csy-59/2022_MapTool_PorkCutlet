using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TileType = MapToolSelecter.ETileType;

public class BasicTile : MonoBehaviour
{
    [SerializeField] private TileType _tileType = TileType.Basic;
    public TileType MyTileType { get => _tileType; }

    private string _playerTag = "Player";
    private bool _isGotData = false;
    protected GameObject _player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(_playerTag))
        {
            _player = other.gameObject;
            GetPlayerData();
        }
    }

    protected virtual void GetPlayerData() 
    {
        _isGotData = true;
    }

    public virtual void OnPlayerEnter() 
    { 
        if(!_isGotData)
        {
            GetPlayerData();
        }
    }
    public virtual void OnPlayerExit() 
    {
        _isGotData = false;
    }
}
