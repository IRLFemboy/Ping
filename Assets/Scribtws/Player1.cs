using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player1 : MonoBehaviour
{

    [Header("Player 1")]
    public Rigidbody rb;
    public float moveSpeed = 20;
    public float horizontal;


    // Start is called before the first frame update
    void Start()
    {
        if (tag == "PlayerOne")
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "PlayerOne")
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector3(horizontal * moveSpeed, 0);
        }
    }
}
