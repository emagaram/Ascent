using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseWithPlayer : MonoBehaviour
{
    public float simpleY;
    public Camera cam;
    public float speed;
    private float origSpeed;
    private PlayerController player;
    // Use this for initialization
    void Start()
    {
        origSpeed = speed;
        cam = FindObjectOfType<Camera>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredVec = calculateDesiredVec();
        if (player.GetComponent<Rigidbody2D>().velocity.magnitude > 20)
        {
            speed = 4.5f * origSpeed;
        }
        else
        {
            speed = origSpeed;
        }
        if (Mathf.Abs(transform.position.y-desiredVec.y)>0.3f)
        {
            transform.position = Vector3.Lerp(transform.position, desiredVec, speed * Time.deltaTime);

        }
    }

    public Vector3 calculateDesiredVec()
    {
        return new Vector3(transform.position.x, simpleY + player.transform.position.y - (player.transform.position.y / 20), transform.position.z);
    }
}
