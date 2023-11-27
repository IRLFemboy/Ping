using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5;
    Rigidbody rb;

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
        float x = Random.Range(0, 2) == 0 ? 1 : -1;
        float y = Random.Range(0, 2) == 0 ? -1 : -1;
        rb.velocity = new Vector3(speed * x, speed * y);
    }
}
