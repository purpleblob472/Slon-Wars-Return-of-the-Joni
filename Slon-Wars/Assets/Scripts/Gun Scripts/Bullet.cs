using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private bool isPlayer;
    private Rigidbody2D body;
    private Vector2 shootersVelocity = Vector2.zero;
    [HideInInspector]
    public int damage = 1;

    //Initiliser for gun script to pass values
    public void SetValues(float speed, bool isPlayer, Vector2 shootersVelocity, float timeAlive, int damage)
    {
        body = GetComponent<Rigidbody2D>();
        this.speed = speed;
        this.isPlayer = isPlayer;
        this.shootersVelocity = shootersVelocity;
        this.damage = damage;
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
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(this.gameObject);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Enemy") && isPlayer)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(this.gameObject);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Boss") && isPlayer)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(this.gameObject);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Wall") || collision.CompareTag("Door"))
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        Vector2 movementVec = transform.up * speed;
        body.velocity = Vector2.ClampMagnitude((movementVec + shootersVelocity), speed);
    }
}
