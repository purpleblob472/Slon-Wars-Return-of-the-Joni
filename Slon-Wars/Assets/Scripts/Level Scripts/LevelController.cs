using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    void Start()
    {

    }

    public void CheckEnemies(bool onEnemyDeath = false)
    {
        int ammountOfEnemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        ammountOfEnemiesAlive -= onEnemyDeath ? 1 : 0;

        if (ammountOfEnemiesAlive > 0)
            CloseAllDoors();
        else
            OpenAllDoors();
    }

    private void OpenAllDoors()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

        for (int i = 0; i < doors.Length; i++)
        {
            GameObject door = doors[i];
            door.GetComponent<SpriteRenderer>().enabled = false;
            door.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void CloseAllDoors()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

        for (int i = 0; i < doors.Length; i++)
        {
            GameObject door = doors[i];
            door.GetComponent<SpriteRenderer>().enabled = true;
            door.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
