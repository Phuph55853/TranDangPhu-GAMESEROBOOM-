using UnityEngine;

public class HoleController : MonoBehaviour
{
    private LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!levelManager.canEnterHole)
            return;

        if (collision.CompareTag("Worm"))
        {
            levelManager.LoadNextLevel();
            Debug.Log("Snake entered hole");
        }
    }
}
