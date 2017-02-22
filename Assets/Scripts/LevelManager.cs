using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public GameObject currentCheckpoint;
	private PlayerController player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
        GlobalControl.Instance.playerLocation = new Vector2(currentCheckpoint.transform.position.x, currentCheckpoint.transform.position.y);
    }

	public IEnumerator RespawnPlayer(float time) {
        Debug.Log("STARTED");
        player.animator.Play("Death");
        player.isDead = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Debug.Log("Player respawned");
        player.attachedToLadder = false;
        yield return new WaitForSeconds(time);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Everest");
        
    }

}
