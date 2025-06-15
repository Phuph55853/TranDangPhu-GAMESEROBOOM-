using System.Collections;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private GameManager gameManager;
    public void Eat()
    {
        Debug.Log("Đã ăn chuối!");

        GameManager gameManager = GetComponentInParent<GameManager>();
        if (gameManager != null)
        {
            gameManager.GrowBody();
        }
        gameObject.SetActive(false);

    }
}
