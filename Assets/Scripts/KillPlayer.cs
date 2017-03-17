using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	public LevelManager levelManager;
    private bool killedPlayer = false;
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
        
	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player" && levelManager!=null && killedPlayer == false) {
            StartCoroutine(levelManager.RespawnPlayer(1));
            killedPlayer = true;
        }
	}
    //void OnCollisionStay2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "Player" && levelManager != null && killedPlayer == false)
    //    {
    //        StartCoroutine(levelManager.RespawnPlayer(1));
    //        killedPlayer = true;
    //    }
    //}
}
