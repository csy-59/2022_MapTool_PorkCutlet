using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToolSelecter : MonoBehaviour
{

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _maxDistance = 3000f;
    [SerializeField] private LayerMask _clickLayer;

    public enum ETileType
    {
        Basic,
        Bush,
        River,
        Wall,
        Max,
    }
    private const int _keyCodeAlphaStart = (int) KeyCode.Alpha1;
    private const int _keyCodeKeypadStart = (int) KeyCode.Keypad1;
    private const int _keyCodeLenth = (int)ETileType.Max;

    private ETileType _currentTileType = ETileType.Basic;

    private void Update()
    {
        GetCurrentTileType();
        GetMouseClickPosition();
    }

    private void GetCurrentTileType()
    {
        for (int i = 0; i < _keyCodeLenth; ++i)
        {
            if (Input.GetKeyDown((KeyCode)(_keyCodeAlphaStart + i)) ||
                Input.GetKeyDown((KeyCode)(_keyCodeKeypadStart + i)))
            {
                _currentTileType = (ETileType)i;
                break;
            }
        }
    }

    private void GetMouseClickPosition()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, _maxDistance ,_clickLayer))
            {
                TileMaster _tileMaster = hit.collider.gameObject.GetComponent<TileMaster>();
                Debug.Assert(_tileMaster);

                _tileMaster.SelectTile(_currentTileType);
            }
        }
    }
}
