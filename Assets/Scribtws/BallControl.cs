using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    Quaternion targetRotation;
    Vector3 rotationAxis = Vector3.zero;
    float launchForce = 40;
    public bool getRotationThing;

    public bool playerHasDirectionPowerup;

    [Header("Starting Values")]
    public float speed = 20;
    [HideInInspector] public Rigidbody rb;

    [Header("Extra")]
    public ParticleSystem redGlowyStuff;
    public ParticleSystem redSpinnyStuff;
    public GameObject arrow;

    

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
            if(Player1.p1Instance.hasPressed)
            {
                if (getRotationThing)
                {
                    getRotationThing = false;
                    targetRotation = transform.rotation;
                    Debug.Log(targetRotation);
                }

                var input = Input.GetAxis("Horizontal") * sensitivity;

                rotationAxis[(int)axis] = input;
                targetRotation *= Quaternion.Euler(rotationAxis);
                targetRotation = ClampAngleOnAxis(targetRotation, (int)axis, -45, 45);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

                if (Input.GetButtonDown("Fire1"))
                {
                    launchForce = speed + 30;
                    AddLotsOfForceThenSlowDown();
                    Player1.p1Instance.hasPowerUp = false;
                    Player1.p1Instance.hasPressed = false;
                    playerHasDirectionPowerup = false;
                    arrow.SetActive(false);
                }
            }

            else if (Player2.p2Instance.hasPressed)
            {
                if (getRotationThing)
                {
                    getRotationThing = false;
                    targetRotation = transform.rotation;
                    Debug.Log(targetRotation);
                }

                var input = Input.GetAxis("Horizontal2") * sensitivity;

                rotationAxis[(int)axis] = input;
                targetRotation *= Quaternion.Euler(rotationAxis);

                targetRotation = ClampAngleOnAxis(targetRotation, (int)axis, 135, 225);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

                if (Input.GetButtonDown("Fire1p2"))
                {
                    launchForce = speed + 30;
                    AddLotsOfForceThenSlowDown();
                    Player2.p2Instance.hasPowerUp = false;
                    Player2.p2Instance.hasPressed = false;
                    playerHasDirectionPowerup = false;
                    arrow.SetActive(false);
                }
            }
            

        }

    }
    Quaternion ClampAngleOnAxis(Quaternion q, int axis, float _minAngle, float _maxAngle)
    {
        if (Player1.p1Instance.hasPressed)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            var angle = 2f * Mathf.Rad2Deg * Mathf.Atan(q[axis]);


            angle = Mathf.Clamp(angle, _minAngle, _maxAngle);
            q[axis] = Mathf.Tan(.5f * Mathf.Deg2Rad * angle);

            return q;
        }
        else if (Player2.p2Instance.hasPressed)
        {
            Vector3 euler = q.eulerAngles;
            euler[axis] = Mathf.Clamp(euler[axis], _minAngle, _maxAngle);
            return Quaternion.Euler(euler);
        }
        else
        {
            return q;
        }
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
            GameManager.instance.goal2Particles.Play();
            Destroy(gameObject);
        }
        else if (other.gameObject == GameManager.instance.goals[0])
        {
            GameManager.instance.p2Score += 1;
            GameManager.instance.goal1Particles.Play();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ParticleSystem redGlowyObject = Instantiate(redGlowyStuff, transform.position, transform.rotation);
    }

    public void AddLotsOfForceThenSlowDown()
    {
        rb.isKinematic = false;
        rb.AddRelativeForce(Vector3.forward * launchForce, ForceMode.VelocityChange);
    }

    
}
