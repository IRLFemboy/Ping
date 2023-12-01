using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIThingy : MonoBehaviour
{
    
    public float moveSpeed;
    public Rigidbody ball;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Ball").GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (ball.velocity.x > 0)
        {
            if (ball.position.y > transform.position.y)
            {
                rb.AddForce(Vector2.up * moveSpeed);

            }
            else if (ball.velocity.y < transform.position.y)
            {
                rb.AddForce(Vector2.down * moveSpeed);

            }
        }
        /*else
        {
            if (transform.position.y > 0)
            {
                rb.AddForce(Vector2.down * moveSpeed);
            }
            else if (transform.position.y < 0)
            {
                rb.AddForce(Vector2.up * moveSpeed);
            }
        }*/
    }
}
