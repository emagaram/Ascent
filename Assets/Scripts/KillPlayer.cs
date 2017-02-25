using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	public LevelManager levelManager;

	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
        
	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player" && levelManager!=null) {
            StartCoroutine(levelManager.RespawnPlayer(1));
        }
	}
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && levelManager != null)
        {
            StartCoroutine(levelManager.RespawnPlayer(1));
        }
    }
}
