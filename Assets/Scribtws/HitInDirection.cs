using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class HitInDirection : MonoBehaviour
{

    /*
  $$$$$$$$\                    $$\            $$$$$$\                      $$\            $$\            $$$$$$\            $$\           
  \__$$  __|                   $$ |          $$  __$$\                     \__|           $$ |          $$  __$$\           $$ |          
     $$ | $$$$$$\   $$$$$$$\ $$$$$$\         $$ /  \__| $$$$$$$\  $$$$$$\  $$\  $$$$$$\ $$$$$$\         $$ /  $$ |$$$$$$$\  $$ |$$\   $$\ 
     $$ |$$  __$$\ $$  _____|\_$$  _|        \$$$$$$\  $$  _____|$$  __$$\ $$ |$$  __$$\\_$$  _|        $$ |  $$ |$$  __$$\ $$ |$$ |  $$ |
     $$ |$$$$$$$$ |\$$$$$$\    $$ |           \____$$\ $$ /      $$ |  \__|$$ |$$ /  $$ | $$ |          $$ |  $$ |$$ |  $$ |$$ |$$ |  $$ |
     $$ |$$   ____| \____$$\   $$ |$$\       $$\   $$ |$$ |      $$ |      $$ |$$ |  $$ | $$ |$$\       $$ |  $$ |$$ |  $$ |$$ |$$ |  $$ |
     $$ |\$$$$$$$\ $$$$$$$  |  \$$$$  |      \$$$$$$  |\$$$$$$$\ $$ |      $$ |$$$$$$$  | \$$$$  |       $$$$$$  |$$ |  $$ |$$ |\$$$$$$$ |
     \__| \_______|\_______/    \____/        \______/  \_______|\__|      \__|$$  ____/   \____/        \______/ \__|  \__|\__| \____$$ |
                                                                               $$ |                                             $$\   $$ |
                                                                               $$ |                                             \$$$$$$  |
                                                                               \__|                                              \______/ 
    */

    public enum Axis
    {
        x = 0,
        y = 1,
        z = 2
    }
    public float sensitivity = 1;
    Axis axis = Axis.y;
    public float rotateSpeed = 5f;
    public float minAngle = -45, maxAngle = 45;
    Quaternion targetRotation;
    Vector3 rotationAxis = Vector3.zero;

    Rigidbody rb;
    public float launchForce;

    public bool is135;

    // Start is called before the first frame update
    void Start()
    {
        targetRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var input = Input.GetAxis("Horizontal") * sensitivity;

        rotationAxis[(int)axis] = input;
        targetRotation *= Quaternion.Euler(rotationAxis);
        targetRotation = ClampAngleOnAxis(targetRotation, (int)axis, minAngle, maxAngle);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        /*if (Input.GetButtonDown("Fire1"))
        {
            rb.AddRelativeForce(Vector3.forward * launchForce, ForceMode.VelocityChange);
        }*/

    }

    Quaternion ClampAngleOnAxis(Quaternion q, int axis, float _minAngle, float _maxAngle)
    {
        if (!is135)
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
        else
        {
            // Convert quaternion to euler angles
            Vector3 euler = q.eulerAngles;

            // Clamp the angle on the specified axis
            euler[axis] = Mathf.Clamp(euler[axis], _minAngle, _maxAngle);

            // Convert euler angles back to quaternion
            return Quaternion.Euler(euler);
        }
        
    }

    private void FixedUpdate()
    {
        /*float rotateY = Input.GetAxisRaw("Horizontal");
        rotateY = Mathf.Clamp(rotateY, -45, 45);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, -rotateY, transform.localEulerAngles.z) * rotateSpeed;

        int rotate = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        Debug.Log(rotate);
        int rotateClamped = Mathf.Clamp(rotate, -45, 45);

        if(rotate != 0)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotateClamped, transform.localEulerAngles.z);
        }

        */

        // $$$$$$\  $$$$$$\ $$\       $$$$$$$$\ $$\   $$\  $$$$$$\  $$$$$$$$\ 
        //$$  __$$\ \_$$  _|$$ |      $$  _____|$$$\  $$ |$$  __$$\ $$  _____|
        //$$ /  \__|  $$ |  $$ |      $$ |      $$$$\ $$ |$$ /  \__|$$ |      
        //\$$$$$$\    $$ |  $$ |      $$$$$\    $$ $$\$$ |$$ |      $$$$$\    
        // \____$$\   $$ |  $$ |      $$  __|   $$ \$$$$ |$$ |      $$  __|          please :3
        //$$\   $$ |  $$ |  $$ |      $$ |      $$ |\$$$ |$$ |  $$\ $$ |      
        //\$$$$$$  |$$$$$$\ $$$$$$$$\ $$$$$$$$\ $$ | \$$ |\$$$$$$  |$$$$$$$$\ 
        // \______/ \______|\________|\________|\__|  \__| \______/ \________|                                                     
    }
}
