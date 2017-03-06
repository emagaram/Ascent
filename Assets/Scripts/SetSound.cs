using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSound : MonoBehaviour {
    public bool turnOn;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            if (coll.GetComponent<AudioSource>() != null)
            {
                if (turnOn)
                {
                    coll.GetComponent<AudioSource>().volume = 1f;
                    if (!coll.GetComponent<AudioSource>().isPlaying)
                    {
                        coll.GetComponent<AudioSource>().Play();
                    }
                }
                else
                {
                    coll.GetComponent<AudioSource>().volume = 0.1f;
                }
            }
        }
    }
}
