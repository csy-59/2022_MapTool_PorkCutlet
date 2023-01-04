using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _mapToolSelecter;
    private bool _isSettingOver = false;

    private void Update()
    {
        if(!_isSettingOver)
        {
            _isSettingOver = Input.GetKeyDown(KeyCode.P);
            if(_isSettingOver)
            {
                _mapToolSelecter.SetActive(false);
                _player.SetActive(true);
            }
        }
    }
}
