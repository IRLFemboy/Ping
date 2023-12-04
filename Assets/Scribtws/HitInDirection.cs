using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitInDirection : MonoBehaviour
{
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        /*float rotateY = Input.GetAxisRaw("Horizontal");
        rotateY = Mathf.Clamp(rotateY, -45, 45);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, -rotateY, transform.localEulerAngles.z) * rotateSpeed;*/

        int rotate = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        Debug.Log(rotate);
        int rotateClamped = Mathf.Clamp(rotate, -45, 45);

        if(rotate != 0)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotateClamped, transform.localEulerAngles.z) * rotateSpeed;

        }
        //transform.Rotate(0, rotateClamped * rotateSpeed * Time.deltaTime, 0);

    }
}
