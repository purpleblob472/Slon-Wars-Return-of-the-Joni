using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private bool isPlayer;
    private Rigidbody2D body;
    private Vector2 shootersVelocity;

    //Initiliser for gun script to pass values
    public void SetValues(float speed, bool isPlayer, Vector2 shootersVelocity, float timeAlive)
    {
        body = GetComponent<Rigidbody2D>();
        this.speed = speed;
        this.isPlayer = isPlayer;
        this.shootersVelocity = shootersVelocity;
        StartCoroutine(DistanceControl(timeAlive));

        if (!isPlayer)
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
            this.tag = "EnemyBullet";
        }
    }

    //Bullet range via time - time runs out destroy bullet
    private IEnumerator DistanceControl(float timeAlive)
    {
        yield return new WaitForSeconds(timeAlive);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPlayer)
            Destroy(gameObject);

        if (collision.CompareTag("Enemy") && isPlayer)
            Destroy(gameObject);

        if (collision.CompareTag("Wall"))
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        Vector2 movementVec = transform.up * speed;
        body.velocity = Vector2.ClampMagnitude((movementVec + shootersVelocity), speed);
    }
}
