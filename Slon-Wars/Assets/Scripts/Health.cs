using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp;
    
    [Range(0, 100)]
    public int percentChanceOfDrops = 25;
    public GameObject[] powerUpDrops;

    public void TakeDamage(GameObject bullet)
    {
        hp -= bullet.GetComponent<Bullet>().damage;

        if (hp <= 0)
        {
            if (gameObject.tag == "Player")
            {
                print("game Over");
                //Add score to game over text
                GameObject gameOverScreen = GameObject.FindGameObjectWithTag("GameOver");
                gameOverScreen.GetComponent<Canvas>().enabled = true;
            }
            else
            {
                LevelController level = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();
                level.CheckEnemies(true);

                //Drop power up
                if (powerUpDrops.Length > 0)
                {
                    if (Random.Range(0, 100) < percentChanceOfDrops)
                    {
                        Instantiate(powerUpDrops[Random.Range(0, powerUpDrops.Length)], transform.position, transform.rotation);
                    }
                }
            }

            Destroy(gameObject);
        }
    }

}

