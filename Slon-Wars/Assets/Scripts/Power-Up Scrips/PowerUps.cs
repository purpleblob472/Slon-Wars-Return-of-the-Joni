using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpValue
{
    PowerUpSpeed,
    PowerUpHP,
    PowerUpStrength,
    PowerUpFire,
    PowerUpShotgun
}

public class PowerUps : MonoBehaviour
{

    public PowerUpValue powerUp;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
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
        else if (powerUp == PowerUpValue.PowerUpFire)
        {
            collision.gameObject.GetComponent<Gun>().fireRate /= 1.1f;
        }
        else if (powerUp == PowerUpValue.PowerUpShotgun)
        {
            collision.gameObject.GetComponent<Gun>().accuracy -= 10;
            collision.gameObject.GetComponent<Gun>().numberOfBullets += 1;
        }

        Destroy(gameObject);
    }
}



