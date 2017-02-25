using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KmsWhenImLowerThan : MonoBehaviour {
    public float limit;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < limit)
        {
            Destroy(gameObject);
        }
    }
}
