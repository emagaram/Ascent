using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public float timeAtRestart;
    public GameObject currentCheckpoint;
    private PlayerController player;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        
        if (currentCheckpoint!=null)
        {
            GlobalControl.Instance.playerLocation = new Vector2(currentCheckpoint.transform.position.x+0.5f, currentCheckpoint.transform.position.y + 3f);
        }
    }

    public IEnumerator RespawnPlayer(float time)
    {
        player.animator.Play("Death");
        player.isDead = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
        player.attachedToLadder = false;
        player.inputDoesntMatter = true;
        yield return new WaitForSeconds(time);
        GlobalControl.Instance.timeAtRestart = Time.time;
        GlobalControl.Instance.timesDied++;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Everest");
    }

}
