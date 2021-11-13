using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private string doorCode;
    private bool isBoss;
    private bool isStart;
    private bool enemiesSpawend;

    public GameObject wallWithDoor;
    public GameObject wall;

    public void InitiliseValue(string doorCode, bool isBoss, bool isStart)
    {
        this.doorCode = doorCode;
        this.isBoss = isBoss;
        this.isStart = isStart;
        this.enemiesSpawend = isStart;

        CreateWalls();

        if(isBoss && isStart)
        {
            Debug.Log("ONE ROOM FLOOR");
        }
    }

    private void CreateWalls()
    {
        string[] directions = { "Up", "Down", "Left", "Right" };

        for (int i = 0; i < directions.Length; i++)
        {
            Transform currentWallPosition = this.transform.Find(directions[i]).gameObject.transform;
            GameObject currentWallPrefab = (doorCode.Contains(directions[i].Substring(0, 1))) ? wallWithDoor : wall;

            GameObject spawnedWall = Instantiate(currentWallPrefab, currentWallPosition.position, currentWallPosition.rotation);
            spawnedWall.transform.parent = currentWallPosition.transform;
        }
    }
}
