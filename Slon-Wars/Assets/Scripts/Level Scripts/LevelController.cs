using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    void Start()
    {

    }

    //Checks if there are any object with the "Enemy" tag - closes doors if not
    //If called by a dying enemy -1 to "Enemy" tag count to equate for that enemy
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
        //Hide all normal doors
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        for (int i = 0; i < doors.Length; i++)
        {
            GameObject door = doors[i];
            door.GetComponent<SpriteRenderer>().enabled = false;
            door.GetComponent<BoxCollider2D>().enabled = false;
        }

        //Show all next level doors
        GameObject[] nextLevelDoors = GameObject.FindGameObjectsWithTag("NextLevelDoor");
        for (int i = 0; i < nextLevelDoors.Length; i++)
        {
            GameObject nextLevelDoor = nextLevelDoors[i];
            nextLevelDoor.GetComponent<SpriteRenderer>().enabled = true;
            nextLevelDoor.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void CloseAllDoors()
    {
        //Show all normal doors
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        for (int i = 0; i < doors.Length; i++)
        {
            GameObject door = doors[i];
            door.GetComponent<SpriteRenderer>().enabled = true;
            door.GetComponent<BoxCollider2D>().enabled = true;
        }

        //Hide all next level doors
        GameObject[] nextLevelDoors = GameObject.FindGameObjectsWithTag("NextLevelDoor");
        for (int i = 0; i < nextLevelDoors.Length; i++)
        {
            GameObject nextLevelDoor = nextLevelDoors[i];
            nextLevelDoor.GetComponent<SpriteRenderer>().enabled = false;
            nextLevelDoor.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
