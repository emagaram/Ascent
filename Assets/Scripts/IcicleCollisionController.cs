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
        if (deadly && other.gameObject.name == "Player" && levelManager != null)
        {
            levelManager.RespawnPlayer();
        }
        //if(other.gameObject.name == "Ground" || other.gameObject.tag == "Ice")
        //{
        //    Destroy(gameObject);
        //}
        if (other.gameObject.tag != "Player")
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
