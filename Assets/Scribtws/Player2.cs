using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{

    [Header("Player 2")]
    public Rigidbody rb2;
    public float moveSpeed2 = 20;
    public float horizontal2;


    // Start is called before the first frame update
    void Start()
    {
        if (tag == "PlayerTwo")
        {
            rb2 = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "PlayerTwo")
        {
            horizontal2 = Input.GetAxisRaw("Horizontal2");
            rb2.velocity = new Vector3(horizontal2 * moveSpeed2, 0);
        }
    }
}
