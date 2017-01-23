using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour {
    public PlayerController player;
    public Transform top;
    public LayerMask playerLayer;
    private float originalPlayerClimbSpeed;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        originalPlayerClimbSpeed = player.climbSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        if (player.ladderTransform == transform)
        {
            if (Physics2D.OverlapCircle(top.position, 0.5f, playerLayer) != null && Physics2D.OverlapCircle(top.position, 0.5f, playerLayer).tag == "Player" && Input.GetAxisRaw("Vertical")==1)
            {
                player.climbSpeed = 0;
            }
            else
            {
                player.climbSpeed = originalPlayerClimbSpeed;
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.onLadder = true;
            player.ladderTransform = transform;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.onLadder = true;
            player.ladderTransform = transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.onLadder = false;
        }
    }
}
