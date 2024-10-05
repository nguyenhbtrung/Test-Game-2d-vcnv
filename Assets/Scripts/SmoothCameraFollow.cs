using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // Nhân vật mà camera sẽ theo dõi
    public float smoothSpeed = 0.125f; // Tốc độ di chuyển mượt mà của camera
    public float offsetX = 0; // Khoảng cách giữa camera và nhân vật theo trục X

    private float fixedY; // Giá trị cố định của trục Y
    private float fixedZ; // Giá trị cố định của trục Z

    void Start()
    {
        // Lấy giá trị Y và Z hiện tại của camera
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Tính toán vị trí mong muốn của camera, chỉ thay đổi trên trục X
            Vector3 desiredPosition = new Vector3(target.position.x + offsetX, fixedY, fixedZ);

            // Sử dụng Lerp để làm mượt di chuyển camera trên trục X
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Cập nhật vị trí của camera
            transform.position = smoothedPosition;
        }
    }
}
