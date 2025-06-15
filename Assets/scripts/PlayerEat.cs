using UnityEngine;

public class PlayerEat : MonoBehaviour
{
    public int score = 0;

    void OnTriggerEnter2D(Collider2D other) // Hoặc OnTriggerEnter nếu là 3D
    {
        if (other.CompareTag("Banana"))
        {
            Destroy(other.gameObject); // Xóa object chuối
            score += 1; // Tăng điểm (hoặc có thể gọi phương thức tăng máu, v.v.)
            Debug.Log("Đã ăn chuối! Điểm: " + score);
        }
    }
}
