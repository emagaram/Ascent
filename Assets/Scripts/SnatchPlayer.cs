using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnatchPlayer : MonoBehaviour
{
    private bool playerAttached;
    public bool playerUnattached = false;
    public float rotationAngle;
    private PlayerController player;
    public GameObject phoenix;
    private bool changedRotation = false;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {

        if (playerAttached && playerUnattached==false)
        {
            player.transform.position = transform.position;
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && playerUnattached == false)
        {
            if (changedRotation == false)
            {
                changedRotation = true;
                phoenix.transform.eulerAngles = new Vector3(0, 0, rotationAngle);
                FindObjectOfType<Camera>().GetComponent<MoveOnPath>().enabled = true;
                FindObjectOfType<Camera>().GetComponent<CameraFollow>().enabled = false;
            }
            FindObjectOfType<RiseWithPlayer>().enabled = false;
            playerAttached = true;
        }
    }
}
