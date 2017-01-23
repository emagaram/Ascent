using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
    public Rigidbody2D projectile;
    public float speed = 0;
    private int counter;
    public int spawnRate = 240;
    public Vector2 direction;

    // Update is called once per frame
    void Update()
    {
        counter++;
        if (counter >= spawnRate)
        {
            counter = 0;
        }
    }

    void FixedUpdate()
    {

        if(counter>=spawnRate)
        {
                Vector2 startingPos = transform.position;
                Rigidbody2D instantiatedProjectile = Instantiate(projectile,
                                                               startingPos,
                                                               Quaternion.identity)
                    as Rigidbody2D;
                Vector2 direction = new Vector2(1, 0f).normalized;
                instantiatedProjectile.AddForce(direction * speed);

        }

    }
}
