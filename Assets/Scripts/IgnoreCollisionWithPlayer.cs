using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionWithPlayer : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D o)
    {
        if (o.gameObject.tag == "Player") {
            StartCoroutine(ignoreColl());
        }
        
    }

    IEnumerator ignoreColl()
    {
        yield return new WaitForEndOfFrame();
        Physics2D.IgnoreCollision(FindObjectOfType<PlayerController>().GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
