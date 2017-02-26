using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutScene : MonoBehaviour {
    public Transform playerTarget;
    public GameObject p;
    public SpriteRenderer dialogueBox;
    public Sprite trespassing;
    public Sprite begone;
    private bool startedTalking;
    public bool cutSceneHasStarted;
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
            p.GetComponent<Animator>().SetBool("cinematic", true);
            p.GetComponent<Animator>().SetBool("jumped", false);
            p.GetComponent<Animator>().SetBool("isMoving", false);
            
            if (!(p.transform.position.x < playerTarget.position.x))
            {
                p.GetComponent<Animator>().Play("Run");
                p.GetComponent<Rigidbody2D>().velocity = new Vector2(-8, p.GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                p.GetComponent<Rigidbody2D>().velocity = new Vector2(0, p.GetComponent<Rigidbody2D>().velocity.y);
                p.GetComponent<Animator>().Play("Idle");
                if (startedTalking == false)
                {
                    StartCoroutine(wizardSpeaking());
                }
                
            }
            p.GetComponent<PlayerController>().enabled = false;
            p.GetComponentInChildren<GroundCheck>().enabled = false;
            p.GetComponentInChildren<WallCheck>().enabled = false;
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
    }

}
