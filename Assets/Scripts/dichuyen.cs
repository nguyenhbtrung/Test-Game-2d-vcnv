using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dichuyen : MonoBehaviour
{
    public float runSpeed = 2f;
    public float jumpSpeed = 3f;
    public Transform groundCheck; // Vị trí để kiểm tra mặt đất
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; // Lớp để xác định mặt đất
    private bool isGrounded;
    Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // Kiểm tra xem nhân vật có đang chạm đất không
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Di chuyển sang phải hoặc trái
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }

        // Kiểm tra nhảy khi nhân vật đang trên mặt đất
        if (isGrounded && Input.GetKeyDown("space"))
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        }
    }

    // Vẽ hình tròn ở vị trí groundCheck trong cửa sổ Scene để kiểm tra
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
