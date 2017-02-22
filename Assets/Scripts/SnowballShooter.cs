using UnityEngine;

public class SnowballShooter : MonoBehaviour
{
    public Rigidbody2D projectile;
    public float speed;
    public Vector2 direction;

    // Update is called once per frame
    void Update()
    {
    }

    public void Fire()
    {

        Vector2 startingPos = transform.position;
        Rigidbody2D instantiatedProjectile = Instantiate(projectile,
                                                       startingPos,
                                                       Quaternion.identity)
            as Rigidbody2D;
        direction = direction.normalized;
        instantiatedProjectile.AddForce(direction * speed);

    }
}
