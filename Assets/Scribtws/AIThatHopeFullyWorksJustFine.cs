using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIThatHopeFullyWorksJustFine : MonoBehaviour
{
    public float moveSpeed;
    Transform ballPos;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        ballPos = GameObject.Find("Ball").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float input = 0;
        Debug.Log("Input:" + input);

        Rigidbody ballRb = ballPos.gameObject.GetComponent<Rigidbody>();
        
        if (ballPos.position.x > transform.position.x)
        {
            input = 1;
            Debug.Log("Input: " + input);
        }
        if (ballPos.position.x < transform.position.x)
        {
            input = -1;
            Debug.Log("Input: " + input);
        }

        Vector3 position = transform.position + new Vector3(input * moveSpeed * Time.deltaTime, 0, 0);
        

        if (input > 1)
        {
            if (position.x > ballPos.position.x) 
            {
                position.x = ballPos.position.x;
            }
        }
        else if (input < 1)
        {
            if(position.x < ballPos.position.x)
            {
                position.x = ballPos.position.x;
            }
        }


        transform.position = position;
    }
}
