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
        if (other.gameObject.tag != "Player")
        {
            if(GetComponentInParent<IcicleFallScript>().gameObject != gameObject)
            {
                Destroy(GetComponentInParent<IcicleFallScript>().gameObject);
            }
            else
            {
                Destroy(gameObject);
            } 
           
        }
    }
}
