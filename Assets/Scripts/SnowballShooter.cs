using UnityEngine;

public class SnowballShooter : MonoBehaviour
{
    public Rigidbody2D projectile;
    public float speed = 10;
    public int counter = 0;
    public int spawnRate = 240;
    public Vector2 direction;

    // Update is called once per frame
    void Update()
    {
        counter++;
    }

    void FixedUpdate()
    {

        if(counter>=spawnRate)
        {
                counter = 0;
                Vector2 startingPos = transform.position;
                Rigidbody2D instantiatedProjectile = Instantiate(projectile,
                                                               startingPos,
                                                               Quaternion.identity)
                    as Rigidbody2D;
                direction = direction.normalized;
                instantiatedProjectile.AddForce(direction * speed);

        }

    }
}
