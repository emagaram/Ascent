using UnityEngine;
using System.Collections;

public class IcicleCollisionController : MonoBehaviour
{
    public bool deadly = true;
    public LevelManager levelManager;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (deadly && other.gameObject.tag == "Player" && levelManager != null)
        {
            StartCoroutine(levelManager.RespawnPlayer(1));
            gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        if (other.gameObject.tag != "Player")
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
