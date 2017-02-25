using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController player;
    //public GameObject currentCollidingObject;
    // Use this for initialization
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<KillPlayer>())
        {
            player.isDead = true;
        }
        if (coll.gameObject.tag != "Checkpoint" && coll.gameObject.tag != "Ladder" && coll.gameObject.tag != "Trigger")
        {
            player.grounded = true;
            if (coll.gameObject.tag == "Ice")
            {
                player.onIce = true;
            }
            if (coll.gameObject.GetComponent<Ramp>() != null)
            {
                player.currentRamp = coll.GetComponent<Ramp>();
            }
        }
    }


    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.GetComponent<KillPlayer>())
        {
            player.isDead = true;
        }
        if (coll.gameObject.tag != "Checkpoint" && coll.gameObject.tag != "Ladder" && coll.gameObject.tag != "Trigger")
        {
            player.grounded = true;
            if (coll.gameObject.tag == "Ice")
            {
                player.onIce = true;
            }
            if (coll.gameObject.GetComponent<Ramp>() != null)
            {
                player.currentRamp = coll.GetComponent<Ramp>();
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Checkpoint" && coll.gameObject.tag != "Ladder" && coll.gameObject.tag != "Trigger")
        {
            player.grounded = false;
            if (coll.gameObject.tag == "Ice")
            {
                player.onIce = false;
            }
            if (coll.gameObject.GetComponent<Ramp>() != null)
            {
                player.currentRamp = null;
            }
        }
    }
}
