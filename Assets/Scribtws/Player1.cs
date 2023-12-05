using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player1 : MonoBehaviour
{
    public static Player1 p1Instance;

    [Header("Movement")]
    [Header("Player 1")]
    public Rigidbody rb;
    public float moveSpeed = 20;
    public float horizontal;

    [Header("Powerup Related")]
    public bool hasPowerUp;
    public bool hasPressed = false;
    public float distanceNeededToStopBall;

    private void Awake()
    {
        p1Instance = this;
    }
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

            //I decided to change a few things so that resulted in having to make this be on the vertical axis and im just lazy to change it

            horizontal = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector3(-horizontal * moveSpeed, 0);
        }

        BallControl ball = GameObject.Find("Ball").GetComponent<BallControl>();
        float distanceFromBall = Vector3.Distance(transform.position, ball.gameObject.transform.position);
        Debug.Log(distanceFromBall);
        if (distanceFromBall < distanceNeededToStopBall && hasPowerUp && !hasPressed && Input.GetButtonDown("Fire2"))
        {
            hasPressed = true;
            ball.rb.velocity = Vector3.zero;
            ball.getRotationThing = true;
            ball.playerHasDirectionPowerup = true;
        }
    }
}
