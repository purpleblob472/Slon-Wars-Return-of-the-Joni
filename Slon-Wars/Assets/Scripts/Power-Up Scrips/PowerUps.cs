using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpValue
{
    PowerUpSpeed,
    PowerUpHP,
    PowerUpStrength
}

public class PowerUps : MonoBehaviour
{

    public PowerUpValue powerUp;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player") return;

        if (powerUp == PowerUpValue.PowerUpSpeed)
        {
            collision.gameObject.GetComponent<PlayerMovement>().speed += 1;
        }
        else if (powerUp == PowerUpValue.PowerUpHP)
        {
            collision.gameObject.GetComponent<Health>().hp += 1;
        }
        else if (powerUp == PowerUpValue.PowerUpStrength)
        {
            collision.gameObject.GetComponent<Gun>().damage += 1;
        }

        Destroy(gameObject);
    }
}



