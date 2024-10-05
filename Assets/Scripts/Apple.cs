using UnityEngine;

public class Apple : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem đối tượng va chạm có phải là Player không
        if (other.CompareTag("Player"))
        {
            // Ẩn quả táo
            gameObject.SetActive(false);
        }
    }
}
