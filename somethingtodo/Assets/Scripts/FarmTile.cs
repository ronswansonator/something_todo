using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class FarmTile : Tile
{
    [System.Serializable]
    public class GrowthLevel
    {
        public Sprite Texture;
        public int DaysTillUpgrade;
    };

    public GrowthLevel[] GrowthLevels;

    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = GrowthLevels[0].Texture;
        FarmManager mgr = FarmManager.GetInstance();
        if(mgr != null)
        {
            int tileAge = mgr.GetTileAge(location);
            GrowthLevel level = GetGrowthLevel(tileAge);
            if ( level != null )
            {
                tileData.sprite = level.Texture;
            }
        }
    }

    GrowthLevel GetGrowthLevel(int age )
    {
        if(age < 0)
        {
            return null;
        }
        GrowthLevel level = null;
        for(int i = 0; i < GrowthLevels.Length; ++i)
        {
            if(GrowthLevels[i].DaysTillUpgrade <= age)
            {
                level = GrowthLevels[i];
            }
        }
        return level;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Custom/Tiles/Farm Tile")]
    public static void CreateRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Make Farm Tile", "New Farm Tile", "Asset", "SaveFarm Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<FarmTile>(), path);
    }
#endif
}
