using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicTile : MonoBehaviour
{
    private string _playerTag = "Player";
    protected GameObject _player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(_playerTag))
        {
            _player = other.gameObject;
            OnPlayerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(_playerTag))
        {
            OnPlayerExit();
        }
    }

    protected abstract void OnPlayerEnter();
    protected abstract void OnPlayerExit();
}
