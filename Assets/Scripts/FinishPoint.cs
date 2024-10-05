using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra xem người chơi va chạm với điểm kết thúc
        {
            FindObjectOfType<GameController>().YouWin(); // Gọi hàm YouWin từ GameController
        }
    }
}
