using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float speed = 20;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine(WaitToLaunch());
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
}
