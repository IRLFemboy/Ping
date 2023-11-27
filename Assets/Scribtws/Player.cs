using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider bc;
    public float moveSpeed;
    public float vertical;

    public float ballLaunchForce = 5;
    public float verticalForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(0, vertical * moveSpeed);

        if(vertical != 0)
        {
            verticalForce = ballLaunchForce / moveSpeed;
        }
        else
        {
            verticalForce = 0;
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();

            ballRb.AddForce(Vector3.right * ballLaunchForce, ForceMode.Impulse);
            ballRb.AddForce(Vector3.up * verticalForce, ForceMode.Impulse);
        }
    }*/
}
