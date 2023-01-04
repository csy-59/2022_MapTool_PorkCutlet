using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushTile : BasicTile
{
    [SerializeField][Range(0f, 1f)] private float _enterPlayerAlphaValue = 0.5f;
    private const float _originalPlayerAlphaValue = 1f;
    private Material _playerMaterial;
    private Color _playerColor;

    protected override void GetPlayerData()
    {
        base.GetPlayerData();

        _playerMaterial = _player.GetComponent<Renderer>().material;
        Debug.Assert(_playerMaterial);
        _playerColor = _playerMaterial.color;
    }

    public override void OnPlayerEnter()
    {
        base.OnPlayerEnter();

        _playerColor.a = _enterPlayerAlphaValue;
        _playerMaterial.color = _playerColor;
    }

    public override void OnPlayerExit()
    {
        base.OnPlayerExit();

        _playerColor.a = _originalPlayerAlphaValue;
        _playerMaterial.color = _playerColor;
    }
}
