using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToolSelecter : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _maxDistance = 3000f;
    [SerializeField] private Material[] _tileMaterials;
    private Color[] _tempTileColor = new Color[(int) ETileType.Max];
    private readonly string _clickTag = "Tile";

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

    private GameObject _focusingTile;
    public GameObject FocusingTile
    {
        get => _focusingTile;
        set
        {
            if(value != _focusingTile)
            {
                if(_isFocusing)
                {
                    _focusingTileMaterial.color = _originalTileColor;
                }

                _focusingTile = value;
                _focusingTileMaterial = _focusingTile.GetComponent<Renderer>().material;
                _originalTileColor = _focusingTileMaterial.color;
                _focusingTileMaterial.color = _tempTileColor[(int)_currentTileType];

                _isFocusing = true;
            }
        }
    }
    private bool _isFocusing = false;
    private Material _focusingTileMaterial;
    private Color _originalTileColor;

    private void Awake()
    {
        for(int i = 0; i<_tileMaterials.Length; ++i)
        {
            _tempTileColor[i] = _tileMaterials[i].color;
            _tempTileColor[i].a = 0.5f;
        }
    }

    private void Update()
    {
        GetCurrentTileType();
        ShowTempTile();
        GetMouseClickPosition();
    }

    private void ShowTempTile()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag(_clickTag)) 
            {
                FocusingTile = hitObject;
            }
            else
            {
                if(_isFocusing)
                {
                    _focusingTileMaterial.color = _originalTileColor;
                    _focusingTile = null;
                    _isFocusing = false;
                }
            }
        }
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
            if(_isFocusing)
            {
                TileMaster _tileMaster = FocusingTile.GetComponentInParent<TileMaster>();
                Debug.Assert(_tileMaster);

                _tileMaster.SelectTile(_currentTileType);
            }
        }
    }
}
