using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverTile : BasicTile
{
    [SerializeField] [Range(0f, 1f)] private float _enterPlayerMovePercentage = 0.5f; 
    private PlayerMovement _playerMovement;

    protected override void GetPlayerData()
    {
        base.GetPlayerData();
        _playerMovement = _player.GetComponent<PlayerMovement>();
        Debug.Assert(_playerMovement);
    }

    public override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        _playerMovement.MoveSpeed *= _enterPlayerMovePercentage;
    }

    public override void OnPlayerExit()
    {
        base.OnPlayerExit();
        _playerMovement.MoveSpeed = _playerMovement.MaxMoveSpeed;
    }
}
