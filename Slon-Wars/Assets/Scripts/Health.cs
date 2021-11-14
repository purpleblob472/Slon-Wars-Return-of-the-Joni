using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp;


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
            }
            Destroy(gameObject);
        }
    }

}

