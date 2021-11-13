using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalEnemy : MonoBehaviour
{
    private LookAtPlayer lookAt;
    private Gun gun;
    private Rigidbody2D body;
    private GameObject player;
    
    private bool newDirection = true;
    private Vector2 direction = Vector2.down;

    public float speed = 2;
    void Start()
    {
        lookAt = this.GetComponent<LookAtPlayer>();
        gun = this.GetComponent<Gun>();
        body = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!player) return;

        lookAt.FacePlayer();
        gun.Shoot();

        //if too close move back
        if(Vector2.Distance(player.transform.position, this.transform.position) <= 2f)
            direction = Vector2.left;

        //if too far move forward
        if (Vector2.Distance(player.transform.position, this.transform.position) > 10f)
            direction = Vector2.right;

        if (newDirection)
        {
            SetDirection();
            newDirection = false;
            StartCoroutine(PickNewDirection());
        }
    }

    void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    //Picks a new random direction for the enemy to move in
    private void SetDirection()
    {
        //0 left, 1 right, 2 up, 3 down
        int randomDirection = Random.Range(0, 4);

        switch (randomDirection)
        {
            case 0:
                direction = Vector2.left;
                break;
            case 1:
                direction = Vector2.right;
                break;
            case 2:
                direction = Vector2.up;
                break;
            default:
                direction = Vector2.down;
                break;
        }
    }

    //Waits a random amount of time then indicates that a new direction is needed
    private IEnumerator PickNewDirection()
    {
        float newDirectionTimer = Random.Range(1f, 4f);
        yield return new WaitForSeconds(newDirectionTimer);
        newDirection = true;
    }
}
