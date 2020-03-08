using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRoll : MonoBehaviour
{

    public float ballSpeed;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

   
    void Update()
    {
        //float xSpeed = Input.GetAxis("Horizontal");
        //float ySpeed = Input.GetAxis("Vertical");
        //rb.AddTorque(new Vector3(xSpeed, 0, ySpeed) * ballSpeed * Time.deltaTime);
    }
}
