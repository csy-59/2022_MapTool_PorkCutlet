using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TileType = MapToolSelecter.ETileType;

public class PlayerStatus : MonoBehaviour
{
    private readonly string _tileTag = "Tile";
    private int[] _tileStack = new int[(int)TileType.Max];

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(_tileTag))
        {
            BasicTile tile = other.gameObject.GetComponent<BasicTile>();
            Debug.Assert(tile);

            if(_tileStack[(int)tile.MyTileType] == 0)
            {
                tile.OnPlayerEnter();
            }

            ++_tileStack[(int)tile.MyTileType];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tileTag))
        {
            BasicTile tile = other.gameObject.GetComponent<BasicTile>();
            Debug.Assert(tile);

            --_tileStack[(int)tile.MyTileType];

            if (_tileStack[(int)tile.MyTileType] == 0)
            {
                tile.OnPlayerExit();
            }
        }
    }
}
