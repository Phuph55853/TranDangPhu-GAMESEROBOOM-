using System.Collections;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    private GameManager gameManager;

    public void Eat()
    {
        gameObject.SetActive(false);

        Debug.Log("Đã ăn thuốc!");


    }
}
