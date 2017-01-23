using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerController>().cantChange = true;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerController>().cantChange = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerController>().cantChange = false;
        }
    }
}
