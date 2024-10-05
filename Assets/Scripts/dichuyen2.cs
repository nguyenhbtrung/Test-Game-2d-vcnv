using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dichuyen2 : MonoBehaviour
{
    public float runSpeed = 2f;
    public float jumpSpeed = 3f;
    Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
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

        // Cho phép nhân vật nhảy bất cứ khi nào người dùng nhấn phím "space"
        if (Input.GetKeyDown("space"))
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        }
    }
}
