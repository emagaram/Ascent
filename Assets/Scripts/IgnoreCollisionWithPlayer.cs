using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionWithPlayer : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D o)
    {
        if (o.gameObject.tag == "Player") {
            Physics2D.IgnoreCollision(o.collider, GetComponent<Collider2D>());
        }
        
    }
}
