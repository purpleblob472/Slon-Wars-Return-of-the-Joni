using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp;
    public int score;

    public void TakeDamage(GameObject bullet)
    {
        hp -= bullet.GetComponent<Bullet>().damage;

        if (hp == 0)
        {
            if (gameObject.tag == "Player")
            {
                print("game Over");
                Application.Quit();
            }
            Destroy(gameObject);
        }
    }

}
