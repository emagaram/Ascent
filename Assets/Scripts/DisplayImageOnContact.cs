using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayImageOnContact : MonoBehaviour {
    public GameObject obj;
    private bool playerContact;
	// Use this for initialization
	void Update () {
        if (playerContact)
        {
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj.gameObject.SetActive(false);
        }
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            playerContact = true;
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            playerContact = false;
        }
    }
}
