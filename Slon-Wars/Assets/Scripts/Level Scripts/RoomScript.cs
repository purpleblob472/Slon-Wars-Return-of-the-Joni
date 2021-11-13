using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    private CameraController mainCamera;
    private LevelController level;

    private string doorCode;
    private bool isBoss;
    private bool isStart;
    private bool enemiesSpawend;

    public GameObject wallWithDoor;
    public GameObject wall;

    public void InitiliseValue(string doorCode, bool isBoss, bool isStart)
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        level = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();

        this.doorCode = doorCode;
        this.isBoss = isBoss;
        this.isStart = isStart;
        this.enemiesSpawend = isStart;

        CreateWalls();

        if (isBoss && isStart)
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

    private void PlayerMoved()
    {
        mainCamera.cameraPos = new Vector3(this.transform.position.x, this.transform.position.y, mainCamera.cameraPos.z);

        if (enemiesSpawend) return;

        if(!isBoss)
        {
            Debug.Log("spawn enemies");
        } 
        else
        {
            Debug.Log("spawn boss");
        }

        enemiesSpawend = true;
        level.CheckEnemies();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMoved();
        }
    }
}
