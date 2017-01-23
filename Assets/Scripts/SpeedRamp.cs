using UnityEngine;
using System.Collections;

public class SpeedRamp : MonoBehaviour
{
    private PlayerController player;
    public float maxHorizontalSpeed;
    private float originalMaxHorizontalSpeed;
    public Transform cannotGoBeyond;
    bool isPlayerContact = false;
    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        originalMaxHorizontalSpeed = player.maxHorizontalSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (player.sr == this)
        {
            if (isPlayerContact)
            {
                player.maxHorizontalSpeed = maxHorizontalSpeed;
            }
            else if (!player.offTheIce && !isPlayerContact)
            {
                player.maxHorizontalSpeed = originalMaxHorizontalSpeed;
            }
            else if (player.offTheIce && transform.rotation.z > 0)
            {
                if (player.offTheIce && player.transform.position.x > cannotGoBeyond.position.x)
                {
                    player.maxHorizontalSpeed = originalMaxHorizontalSpeed;
                }
            }

            else if (player.offTheIce && transform.rotation.z < 0)
            {
                if (player.offTheIce && player.transform.position.x < cannotGoBeyond.position.x)
                {
                    player.maxHorizontalSpeed = originalMaxHorizontalSpeed;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            isPlayerContact = true;
            player.sr = this;
        }
    }
    void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            isPlayerContact = false;
        }
    }
}
