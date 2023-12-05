using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public static Player2 p2Instance;

    [Header("Player 2")]
    public Rigidbody rb2;
    public float moveSpeed2 = 20;
    public float horizontal2;

    public bool hasPowerUp;
    public bool hasPressed = false;
    public float distanceNeededToStopBall;

    public int powerMeter = 0;
    public int powerMeterMax;
    public PowerBar powerBar;

    private void Awake()
    {
        p2Instance = this;
    }
    void Start()
    {
        if (tag == "PlayerTwo")
        {
            rb2 = GetComponent<Rigidbody>();
        }

        powerMeter = 0;
        powerBar.setPowerMax(powerMeterMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "PlayerTwo")
        {
            horizontal2 = Input.GetAxisRaw("Vertical2");
            rb2.velocity = new Vector3(-horizontal2 * moveSpeed2, 0);
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
