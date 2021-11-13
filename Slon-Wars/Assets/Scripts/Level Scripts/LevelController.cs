using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CheckEnemies()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
            Debug.Log("Enemies Exist");
        else
            Debug.Log("All Enemies Killed");
    }
}
