using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmManager : MonoBehaviour
{
    class FarmSpotInstance
    {
        public FarmTile farmTile;
        public int currentDay;
    }

    static FarmManager _instance;
    public static FarmManager GetInstance() { return _instance; }

    public Tilemap BaseLayer;
    public Tilemap FarmLayer;
    public TileBase farmableTile;

    private Dictionary<Vector3Int, FarmSpotInstance> _tileset = new Dictionary<Vector3Int, FarmSpotInstance>();

    private void Awake()
    {
        _instance = this;
    }


    public void Plant(FarmTile tile, Vector2 worldPos)
    {
        Vector3Int tileId = BaseLayer.WorldToCell(worldPos);
        TileBase groundTile = BaseLayer.GetTile(tileId);
        if (groundTile == farmableTile)
        {
            if (FarmLayer.GetTile(tileId) == null)
            {
                FarmLayer.SetTile(tileId, tile);
                FarmSpotInstance inst = new FarmSpotInstance();
                inst.farmTile = tile;
                inst.currentDay = 0;
                _tileset.Add(tileId, inst);
            }
        }
    }

    public int GetTileAge(Vector3Int loc)
    {
        FarmSpotInstance t;
        if(_tileset.TryGetValue(loc, out t))
        {
            return t.currentDay;
        }
        return -1;
    }

    public void OnNextDay()
    {
        foreach(FarmSpotInstance inst in _tileset.Values)
        {
            inst.currentDay += 1;
        }

        FarmLayer.RefreshAllTiles();
    }
}
