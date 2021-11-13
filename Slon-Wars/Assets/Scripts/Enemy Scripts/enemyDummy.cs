using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDummy : MonoBehaviour
{
    private LookAtPlayer lookAt;
    private Gun gun;
    void Start()
    {
        lookAt = this.GetComponent<LookAtPlayer>();
        gun = this.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        lookAt.FacePlayer();
        gun.Shoot();
    }
}
