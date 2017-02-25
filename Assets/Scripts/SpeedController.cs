using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float maxSpeed;
    private Rigidbody2D rb;
    public Vector2 myVelocity;
    public bool useVelocity = false;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (useVelocity == true)
        {
            rb.velocity = myVelocity;
            useVelocity = false;
            rb.AddTorque(-2000, ForceMode2D.Impulse);
        }
        
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
