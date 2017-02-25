using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKill : MonoBehaviour {
       
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            StartCoroutine(FindObjectOfType<LevelManager>().RespawnPlayer(1));
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            StartCoroutine(FindObjectOfType<LevelManager>().RespawnPlayer(1));
        }
    }
}
