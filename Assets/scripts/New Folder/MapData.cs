using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Game/Map Data")]
public class MapData : ScriptableObject
{
    [System.Serializable]
    public class MapEntry
    {
        public string mapName;
        public GameObject mapPrefab;
        public Vector3 spawnPosition;
    }

    public MapEntry[] maps;
}
