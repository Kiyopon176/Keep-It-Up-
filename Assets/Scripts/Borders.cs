using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Player");
        rb = ball.GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            rb.AddForce(new Vector2(-rb.velocity.x * 2, rb.velocity.y));
            print("Ball");
        }
    }
}
