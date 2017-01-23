using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	public LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player" && levelManager!=null) {
            StartCoroutine(WaitBeforeDeath(1f));
		}
	}

    IEnumerator WaitBeforeDeath(float time)
    {
        FindObjectOfType<PlayerController>().animator.Play("Death");
        FindObjectOfType<PlayerController>().isDead = true;
        yield return new WaitForSeconds(time);
        levelManager.RespawnPlayer();
        
    }
}
