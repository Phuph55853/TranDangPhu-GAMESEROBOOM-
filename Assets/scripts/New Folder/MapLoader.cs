using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public MapData mapData;
    public GameObject currentMap;

    public void LoadMap(string mapName)
    {
        if (currentMap != null)
        {
            Destroy(currentMap);
        }

        foreach (var map in mapData.maps)
        {
            if (map.mapName == mapName)
            {
                currentMap = Instantiate(map.mapPrefab);
                return;
            }
        }

        Debug.LogWarning("Map not found: " + mapName);
    }
}
