using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class Player : MonoBehaviour
{
    public static Player pInstance;

    [Header("Movement")]
    [Header("Player 1")]
    public Rigidbody rb;
    public float moveSpeed = 20;
    public float vertical;

    [Header("Powerup Related")]
    public bool hasPowerUp = false;
    public bool hasPressed = false;
    public float distanceNeededToStopBall;

    public int powerMeter = 0;
    public int powerMeterMax;
    public PowerBar powerBar;


    private void Awake()
    {
        pInstance = this;
    }
    void Start()
    {
        if (tag == "PlayerOne")
        {
            rb = GetComponent<Rigidbody>();
        }

        powerMeter = 0;
        powerBar.setPowerMax(powerMeterMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "PlayerOne")
        {

            //I decided to change a few things so that resulted in having to make this be on the vertical axis and im just lazy to change it

            vertical = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector3(-vertical * moveSpeed, 0);
        }

        if (powerMeter == powerMeterMax)
        {
            hasPowerUp = true;
        }

        GameObject ball = GameManager.instance.ballGameObject;
        BallControl ballControl = ball.GetComponent<BallControl>();
        float distanceFromBall = Vector3.Distance(transform.position, ball.gameObject.transform.position);
        if (distanceFromBall < distanceNeededToStopBall && hasPowerUp && !hasPressed && Input.GetButtonDown("Fire2"))
        {
            hasPressed = true;
            powerMeter = 0;
            powerBar.setPower(powerMeter);
            ballControl.rb.velocity = Vector3.zero;
            ballControl.getRotationThing = true;
            ballControl.playerHasDirectionPowerup = true;
            ballControl.arrow.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            powerMeter += 10;
            powerBar.setPower(powerMeter);

            if (powerMeter > powerMeterMax)
            {
                powerMeter = powerMeterMax;
            }
        }
    }
}
