using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObjectRotation : MonoBehaviour {
    public GameObject obj;
    public Transform point;
    public float rotationAngle;
    public bool flipX;
    public CircleCollider2D coll;
    private bool changed = false;
    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(point.position, coll.radius) == obj.GetComponent<Collider2D>() && changed == false)
        {
            obj.transform.eulerAngles = new Vector3(0, 0, rotationAngle);
            if (flipX)
            {
                Vector3 localScale = obj.transform.localScale;
                localScale.x = -obj.transform.localScale.x;
                obj.transform.localScale = localScale;
            }
            changed = true;
        }
    }
}
