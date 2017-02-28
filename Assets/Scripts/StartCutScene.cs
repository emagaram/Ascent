using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutScene : MonoBehaviour {
    public Transform playerTarget;
    public GameObject player;
    private bool isFading = false;
    public GameObject wizard;
    public SpriteRenderer dialogueBox;
    public Sprite trespassing;
    public Sprite finalQuote;
    public Sprite begone;
    private bool startedTalking;
    public GameObject phoenix;
    public bool cutSceneHasStarted;
    public GameObject blackSquare;
    private bool phoenixAnimating = false;
    private bool changedRotation = false;
    public float rotationAngle;

    void Start()
    {
        phoenix.GetComponent<Animator>().speed = 0;
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
        Physics2D.IgnoreCollision(phoenix.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        if (isFading)
        {
            Color newColor = blackSquare.GetComponent<SpriteRenderer>().color;
            newColor.a = Mathf.Lerp(newColor.a, 1, Time.deltaTime * 1.4f);
            blackSquare.GetComponent<SpriteRenderer>().color = newColor;
            if (newColor.a > 0.999)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
            }
        }
        if (cutSceneHasStarted)
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
               
                phoenix.GetComponent<MoveOnPath>().enabled = true;
                phoenixAnimating = false;
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
        if (changedRotation == false)
        {
            phoenix.transform.eulerAngles = new Vector3(0, 0, rotationAngle);
            changedRotation = true;
        }
        phoenixAnimating = true;
        yield return new WaitForSeconds(8f);
        if (wizard.transform.localScale.x < 0)
        {
            Vector3 localScale = wizard.transform.localScale;
            localScale.x = Mathf.Abs(wizard.transform.localScale.x);
            wizard.transform.localScale = localScale;
        }
        yield return new WaitForSeconds(2f);
        dialogueBox.enabled = true;
        dialogueBox.sprite = finalQuote;
        yield return new WaitForSeconds(5f);
        dialogueBox.enabled = false;
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<CameraFollow>().enabled = false;
        yield return new WaitForSeconds(1.4f);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<Rigidbody2D>().gravityScale = 1.2f;
        phoenix.GetComponentInChildren<SnatchPlayer>().playerUnattached = true;
        yield return new WaitForSeconds(1.5f);
        isFading = true;


    }

}
