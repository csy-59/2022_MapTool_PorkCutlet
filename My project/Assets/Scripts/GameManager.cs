using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _mapToolSelecter;
    private MapToolSelecter _mapToolSelecterScript;
    private bool _isSettingOver = false;

    private void Awake()
    {
        _mapToolSelecterScript = _mapToolSelecter.GetComponent<MapToolSelecter>();
    }

    private void Update()
    {
        if(!_isSettingOver)
        {
            _isSettingOver = Input.GetKeyDown(KeyCode.P);
            if(_isSettingOver)
            {
                _mapToolSelecterScript.EndMaping();
                _mapToolSelecter.SetActive(false);
                _player.SetActive(true);
            }
        }


        if(Input.GetKeyDown(KeyCode.Q))
        {
            _mapToolSelecterScript.SaveMap();
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            _mapToolSelecterScript.LoadMap();
        }
    }
}
