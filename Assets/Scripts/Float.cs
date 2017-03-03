using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public Transform left;
    public Transform right;
    public float speed;
    private Rigidbody2D rb;
    public bool goLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (goLeft)
        {
            if (Mathf.Abs(transform.position.x - left.position.x) < 1f && rb.velocity.x<0)
            {
                goLeft = false;
            }
            rb.velocity = new Vector2(-speed,0);
        }
        else
        {
            if (Mathf.Abs(transform.position.x - right.position.x) < 1f && rb.velocity.x > 0)
            {
                goLeft = true;
            }
            rb.velocity = new Vector2(speed, 0);
        }
    }
}
