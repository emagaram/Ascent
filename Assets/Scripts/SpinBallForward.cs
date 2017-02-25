using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBallForward : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<KmsWhenImLowerThan>() != null)
        {
            coll.GetComponent<Rigidbody2D>().AddTorque(-2000, ForceMode2D.Impulse);
        }
    }
}
