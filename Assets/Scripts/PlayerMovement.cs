using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalSpeed;
    public float forwardSpeed;
    private Rigidbody rb;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        playerMovement();      
    }

 
    private void playerMovement()
    {

        float h = Input.GetAxis("Horizontal") * horizontalSpeed;       
        Vector3 vel = rb.velocity;
        vel.x = h;
        vel.z = forwardSpeed;

        rb.velocity = vel;
    }
 

}
