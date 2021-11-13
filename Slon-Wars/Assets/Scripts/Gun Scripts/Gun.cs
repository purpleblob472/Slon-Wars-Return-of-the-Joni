using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawn;

    public bool isEnemy;
    public float fireRate = 2;
    public float minfireRate = 0.2f;
    public float range = 2;
    public float accuracy = 180;
    public float bulletSpeed = 5;
    public float numberOfBullets = 1;
    public int damage = 1;

    private bool canFire;
    private int bulletRotationOffset = 0;
    private Rigidbody2D body;

    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();

        if (!bulletSpawn)
            bulletSpawn = transform;

        if (!isEnemy)
        {
            canFire = true;
        }
        else
        {
            bulletRotationOffset = 90;
            StartCoroutine(canFireDelay());
        }
    }

    //function to call from other scripts
    public void Shoot()
    {
        if (canFire)
            StartCoroutine(fireRateTimer());
    }

    //stops enemies from firing on spawn
    private IEnumerator canFireDelay()
    {
        canFire = false;
        fireRate = (fireRate <= minfireRate) ? minfireRate : fireRate;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    //fire rate control
    private IEnumerator fireRateTimer()
    {
        FireBullets();
        canFire = false;
        fireRate = (fireRate <= minfireRate) ? minfireRate : fireRate;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    //handle spawning bullets and assigning bullet properties
    private void FireBullets()
    {
        accuracy = (accuracy <= 0) ? 0 : accuracy;
        float bulletRotationModifier = accuracy > 180 ? 0 : (180 - accuracy);

        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject currentBullet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            currentBullet.transform.eulerAngles = new Vector3(
                currentBullet.transform.eulerAngles.x,
                currentBullet.transform.eulerAngles.y,
                currentBullet.transform.eulerAngles.z + Random.Range(-bulletRotationModifier, bulletRotationModifier) - bulletRotationOffset
            );
            currentBullet.GetComponent<Bullet>().SetValues(bulletSpeed, !isEnemy, body.velocity, range, damage);
        }
    }
}
