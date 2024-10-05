using UnityEngine;

public class Trap : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra xem người chơi va chạm với bẫy
        {
            FindObjectOfType<GameController>().GameOver(); // Gọi hàm GameOver từ GameController
        }
    }
}
