using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public MapData mapData;
    public MapLoader mapLoader;
    public GameObject holeClosedSprite;
    public GameObject holeOpenSprite;
    public bool canEnterHole = false;

    private int currentLevelIndex = 0;

    void Start()
    {
        LoadLevel(currentLevelIndex);
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < mapData.maps.Length)
        {
            currentLevelIndex = levelIndex;

            // Xóa map cũ nếu có
            if (mapLoader.currentMap != null)
            {
                Destroy(mapLoader.currentMap);
            }

            // Instantiate map prefab tại vị trí spawnPosition
            var mapEntry = mapData.maps[levelIndex];
            mapLoader.currentMap = Instantiate(mapEntry.mapPrefab, mapEntry.spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.Log("Không còn level để load!");
        }
    }

    public void Win()
    {
        Debug.Log("You Win!");
        holeClosedSprite.SetActive(false);
        Debug.Log("holeClosedSprite active: " + holeClosedSprite.activeSelf);

        holeOpenSprite.SetActive(true);
        Debug.Log("holeOpenSprite active: " + holeOpenSprite.activeSelf);

        var srOpen = holeOpenSprite.GetComponent<SpriteRenderer>();
        if (srOpen != null)
        {
            Debug.Log($"holeOpenSprite sortingOrder: {srOpen.sortingOrder}");
        }

        if (holeOpenSprite.transform.parent != null)
        {
        }

        canEnterHole = true;
        Debug.Log("Bạn đã chiến thắng level " + currentLevelIndex);
        LoadLevel(currentLevelIndex + 1);
    }
    public void LoadNextLevel()
    {
        LoadLevel(currentLevelIndex + 1);
    }

}
