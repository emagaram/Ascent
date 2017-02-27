using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnatchPlayer : MonoBehaviour {
    private bool playerAttached;
    public float rotationAngle;
    private PlayerController player;
    public GameObject phoenix;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (playerAttached)
        {
            phoenix.transform.eulerAngles = new Vector3(0, 0, rotationAngle);
            player.transform.position = transform.position;
            player.GetComponent<Animator>().Play("Idle");
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            FindObjectOfType<CameraFollow>().enabled = false;
            FindObjectOfType<RiseWithPlayer>().enabled = false;
            playerAttached = true;
        }
    }
}
