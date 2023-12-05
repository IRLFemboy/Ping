using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{

    public enum Axis
    {
        x = 0,
        y = 1,
        z = 2
    }

    [Header("Hit Direction Powerup")]
    public float sensitivity = 1;
    Axis axis = Axis.y;
    public float rotateSpeed = 5f;
    float minAngle = -45, maxAngle = 45;
    Quaternion targetRotation;
    Vector3 rotationAxis = Vector3.zero;
    float launchForce;

    public bool playerHasDirectionPowerup;

    [Header("Starting Values")]
    public float speed = 20;
    [HideInInspector] public Rigidbody rb;

    public ParticleSystem blueGlowyStuff;

    public bool getRotationThing;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine(WaitToLaunch());
    }

    private void Update()
    {
        if (playerHasDirectionPowerup)
        {
            if (getRotationThing)
            {
                if (Player1.p1Instance.hasPowerUp)
                {
                    transform.localEulerAngles = Vector3.zero;
                }
                else if (Player2.p2Instance.hasPowerUp)
                {
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                }

                getRotationThing = false;
                targetRotation = transform.rotation;
                Debug.Log(targetRotation);
            }

            var input = Input.GetAxis("Horizontal") * sensitivity;

            rotationAxis[(int)axis] = input;
            targetRotation *= Quaternion.Euler(rotationAxis);
            targetRotation = ClampAngleOnAxis(targetRotation, (int)axis, minAngle, maxAngle);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Fire1"))
            {
                launchForce = speed;
                rb.AddRelativeForce(Vector3.forward * launchForce, ForceMode.Impulse);
                Player1.p1Instance.hasPowerUp = false;
                Player1.p1Instance.hasPressed = false;
                Player2.p2Instance.hasPowerUp = false;
                playerHasDirectionPowerup = false;
            }

        }

    }
    Quaternion ClampAngleOnAxis(Quaternion q, int axis, float _minAngle, float _maxAngle)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        var angle = 2f * Mathf.Rad2Deg * Mathf.Atan(q[axis]);

        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        q[axis] = Mathf.Tan(.5f * Mathf.Deg2Rad * angle);

        return q;
    }

    void Launch()
    {
        float x = Random.value < .5f ? -1 : 1;
        float z = Random.value < .5f ? Random.Range(-1, -.5f) :
                                       Random.Range(.5f, 1f);

        Vector3 direction = new Vector3(x, 0, z);
        rb.AddForce(direction * speed, ForceMode.Impulse);
    }

    IEnumerator WaitToLaunch()
    {
        yield return new WaitForSeconds(1);
        Launch();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.goals[1])
        {
            GameManager.instance.p1Score += 1;
            Destroy(gameObject);
        }
        else if (other.gameObject == GameManager.instance.goals[0])
        {
            GameManager.instance.p2Score += 1;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ParticleSystem blueGlowyObject = Instantiate(blueGlowyStuff, transform.position, transform.rotation);
    }
}
