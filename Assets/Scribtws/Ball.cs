using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody rb;
    public float launchForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Launch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch()
    {
        float x = Random.value < .5f ? -1 : 1;
        float y = Random.value < .5f ? Random.Range(-1, -.5f) :
                                       Random.Range(.5f, 1f);

        Vector2 direction = new Vector2(x, y);
        rb.AddForce(direction * speed, ForceMode.Acceleration);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pong"))
        {
            if (transform.position.x < 0)
            {
                rb.AddForce(Vector3.right * launchForce, ForceMode.Impulse);
            }
            else if (transform.position.x > 0)
            {
                rb.AddForce(Vector3.left * launchForce, ForceMode.Impulse);
            }
        }
    }*/
}
