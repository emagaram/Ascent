using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChanger : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<SpeedController>() != null)
        {
            col.GetComponent<SpeedController>().maxSpeed = 1000;
        }
    }
}
