using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private Gun gun;
    private Rigidbody2D body;
    private int maxhp;
    private bool supersaiyan;

    // Start is called before the first frame update
    void Start()
    {
        gun = this.GetComponent<Gun>();
        body = this.GetComponent<Rigidbody2D>();
        maxhp = gameObject.GetComponent<Health>().hp;
    }
        // Update is called once per frame
    void Update()
    {
        gun.Shoot();
        if (gameObject.GetComponent<Health>().hp <= (maxhp / 2) && !supersaiyan)
        {
            gameObject.GetComponent<Gun>().fireRate /= 2f;
            gameObject.GetComponent<Gun>().numberOfBullets *= 1.5f;
            gameObject.GetComponent<Gun>().range *= 2;
            gameObject.GetComponent<Gun>().bulletSpeed /= 1.5f;
            supersaiyan = true;
        }
    }
}
