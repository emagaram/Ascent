using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollExcForPlayer : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            Physics2D.IgnoreCollision(coll.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2(GetComponentInParent<Rigidbody2D>().velocity.x, GetComponentInParent<Rigidbody2D>().velocity.y - 17);
        }

    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            Physics2D.IgnoreCollision(coll.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
