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

	public void RespawnPlayer() {
		Debug.Log ("Player respawned");
        player.attachedToLadder = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Everest");
    }

}
