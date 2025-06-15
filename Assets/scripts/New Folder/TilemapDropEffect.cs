using UnityEngine;
using DG.Tweening;

public class TilemapDropEffect : MonoBehaviour
{
    public float delayPerRow = 0.2f;
    public float dropDistance = 2f;
    public float dropDuration = 0.5f;

    private void Start()
    {
        // Tìm tất cả Tilemap con (Layer1, Layer2, ...)
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform row = transform.GetChild(i);

            Vector3 originalPos = row.position;
            row.position += Vector3.up * dropDistance;

            // Delay theo thứ tự hàng
            float delay = i * delayPerRow;

            row.DOMoveY(originalPos.y, dropDuration)
                .SetEase(Ease.OutBounce)
                .SetDelay(delay);

        }

    }
}
