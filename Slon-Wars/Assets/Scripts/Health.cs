using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Player" && collision.gameObject.tag == "EnemyBullet") { hp -= collision.gameObject.GetComponent<Bullet>().damage; }
        else if (gameObject.tag == "Enemy" && collision.gameObject.tag == "PlayerBullet") { hp -= collision.gameObject.GetComponent<Bullet>().damage; }
    }
}
