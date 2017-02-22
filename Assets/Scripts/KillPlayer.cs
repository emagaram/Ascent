using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	public LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
        
	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player" && levelManager!=null) {
            StartCoroutine(levelManager.RespawnPlayer(1));
        }
	}
}
