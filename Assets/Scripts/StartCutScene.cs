using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutScene : MonoBehaviour {
    public Transform playerTarget;
    public Transform flyAwayTarget;
    public GameObject player;
    public SpriteRenderer dialogueBox;
    public Sprite trespassing;
    public Sprite begone;
    private bool startedTalking;
    public GameObject phoenix;
    private Transform phoenixStartingPos;
    public Transform phoenixUp;
    public bool cutSceneHasStarted;
    private bool phoenixAnimating = false;
    private bool check = true;
    private bool targetHeadNow = false;
    private Vector3 phoenixUpPos;
    private Vector3 phoenixPlayerPos;
    public float rotationAngle;

    void Start()
    {
        phoenix.GetComponent<Animator>().speed = 0;
        phoenixStartingPos = phoenix.transform;
        //StartCoroutine(PhoenixAnimation());

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            cutSceneHasStarted = true;
        }
    }

    void Update()
    {
        if(cutSceneHasStarted)
        {
            player.GetComponent<Animator>().SetBool("cinematic", true);
            player.GetComponent<Animator>().SetBool("jumped", false);
            player.GetComponent<Animator>().SetBool("isMoving", false);
            if (player.transform.localScale.x > 0)
            {
                player.GetComponent<PlayerController>().Flip();
            }
            if (!(player.transform.position.x < playerTarget.position.x) && !startedTalking)
            {
                player.GetComponent<Animator>().Play("Run");
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(-8, player.GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
                player.GetComponent<Animator>().Play("Idle");
                if (startedTalking == false)
                {
                    StartCoroutine(wizardSpeaking());
                }
                
            }
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponentInChildren<GroundCheck>().enabled = false;
            player.GetComponentInChildren<WallCheck>().enabled = false;
            if(phoenixAnimating == true && player.transform.position.x < playerTarget.position.x)
            {
                phoenix.transform.eulerAngles = new Vector3(0, 0, rotationAngle);
                phoenix.GetComponent<MoveOnPath>().enabled = true;
            }
        }
    }

    IEnumerator wizardSpeaking()
    {
        startedTalking = true;
        yield return new WaitForSeconds(2f);
        dialogueBox.enabled = true;
        yield return new WaitForSeconds(4f);
        dialogueBox.sprite = trespassing;
        yield return new WaitForSeconds(4f);
        dialogueBox.sprite = begone;
        yield return new WaitForSeconds(2f);
        dialogueBox.enabled = false;
        yield return new WaitForSeconds(2f);
        phoenix.GetComponent<Animator>().speed = 1;
        yield return new WaitForSeconds(0.7f);
        phoenix.GetComponent<Animator>().Play("PhoenixFly");
        phoenixAnimating = true;

    }

}
