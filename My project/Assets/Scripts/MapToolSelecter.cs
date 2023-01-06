using System;
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

    // 타일 입력 관련
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

    // 현재 보고 있는 타일
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

                    if (_isDraging)
                    {
                        SetTileToType(value);
                    }
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

    // maping 내용
    [SerializeField] private GameObject _tiles;
    [SerializeField] private int _mapSize;
    private TileMaster[] _tileMap;
    private int[][] _map;
    private bool _isMapModified = false;
    private bool _isDraging = false;

    [Serializable]
    public class MapInfo
    {
        // 맵 크기
        public int MapSize;
        // csv 파일 형식 맵 파일
        public string MapFileName;
    }
    private MapInfo _mapInfo = new MapInfo();

    private readonly string _mapInfoName = "MapInfo";
    private string _mapFileName = "Map";

    private void Awake()
    {
        for (int i = 0; i < _tileMaterials.Length; ++i)
        {
            _tempTileColor[i] = _tileMaterials[i].color;
            _tempTileColor[i].a = 0.5f;
        }

        _map = new int[_mapSize][];
        for (int i = 0; i < _mapSize; ++i)
        {
            _map[i] = new int[_mapSize];
        }

        _tileMap = _tiles.GetComponentsInChildren<TileMaster>();

        int tileSize = _tileMap.Length;
        for (int i = 0; i < tileSize; ++i)
        {
            _tileMap[i].TileNumber = i;
        }
    }

    private void Update()
    {
        GetCurrentTileType();
        ShowTempTile();
        GetMouseDrag();
    }

    private void GetCurrentTileType()
    {
        for (int i = 0; i < _keyCodeLenth; ++i)
        {
            if (Input.GetKeyDown((KeyCode)(_keyCodeAlphaStart + i)) ||
                Input.GetKeyDown((KeyCode)(_keyCodeKeypadStart + i)))
            {
                _currentTileType = (ETileType)i;

                if(_isFocusing)
                {
                    _focusingTileMaterial.color = _tempTileColor[(int)_currentTileType];
                }
                
                break;
            }
        }
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
                ResetTile();
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
                int tileNumber = _tileMaster.TileNumber;
                _map[tileNumber / _mapSize][tileNumber % _mapSize] = (int)_currentTileType;
                _isMapModified = true;
            }
        }
    }
    private void GetMouseDrag()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(_isFocusing)
            {
                SetTileToType(FocusingTile);
                _isDraging = true;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            _isDraging = false;
        }
    }

    private void SetTileToType(GameObject tile)
    {
        TileMaster tileMaster = tile.GetComponentInParent<TileMaster>();
        Debug.Assert(tileMaster);

        tileMaster.SelectTile(_currentTileType);
        int tileNumber = tileMaster.TileNumber;
        _map[tileNumber / _mapSize][tileNumber % _mapSize] = (int)_currentTileType;
        _isMapModified = true;
    }

    public void EndMaping()
    {
        ResetTile();
    }

    private void ResetTile()
    {
        if (_isFocusing)
        {
            _focusingTileMaterial.color = _originalTileColor;
            _focusingTile = null;
            _isFocusing = false;
        }
    }

    public void LoadMap()
    {
        if(_isMapModified)
        {
            return;
        }

        _mapInfo = JsonParse<MapInfo>.Load(_mapInfo, _mapInfoName);
        _mapFileName = _mapInfo.MapFileName;
        _mapSize = _mapInfo.MapSize;

        string[][] mapString = CSVParse<int>.CSVToArrayOf2D(_mapFileName);
        for (int i = 0; i < mapString.Length; ++i)
        {
            for (int j = 0; j < mapString[i].Length; ++j)
            {
                _map[i][j] = int.Parse(mapString[i][j]);
                _tileMap[i * _mapSize + j].SelectTile((ETileType)_map[i][j]);
            }
        }
    }

    public void SaveMap()
    {
        _mapInfo.MapSize = _mapSize;
        _mapInfo.MapFileName = _mapFileName;
        CSVParse<int>.ArrayOf2DToCSV(_map, _mapSize, _mapFileName);
        JsonParse<MapInfo>.Save(_mapInfo, _mapInfoName);

        _isMapModified = true;
    }
}
