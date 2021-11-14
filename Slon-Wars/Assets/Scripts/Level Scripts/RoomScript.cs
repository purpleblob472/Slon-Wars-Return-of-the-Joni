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
    public GameObject nextLevelDoor;

    public GameObject bunchOfPowerUps;

    public int minEmenySpawns = 1;
    public int maxEmenySpawns = 5;

    public GameObject[] enemies;
    public GameObject[] bosses;

    public void InitiliseValue(string doorCode, bool isBoss, bool isStart)
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        level = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();

        this.doorCode = doorCode;
        this.isBoss = isBoss;
        this.isStart = isStart;
        this.enemiesSpawend = isStart;

        CreateWalls();

        if (isBoss)
            SpawnNextLevelDoor();

        //Level generator can create a 1 room floor
        //Spawn a treat for player if this happens
        if (isBoss && isStart)
            SpawnBunchOfPowerUps();
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

    private void SpawnEnemies()
    {
        int amountOfEnemies = Random.Range(minEmenySpawns, maxEmenySpawns + 1);

        for (int i = 0; i < amountOfEnemies; i++)
        {
            int enemyToSpawn = Random.Range(0, enemies.Length);
            Instantiate(enemies[enemyToSpawn], transform.position, transform.rotation);
        }
    }

    private void SpawnNextLevelDoor()
    {
        GameObject spawnedDood = Instantiate(nextLevelDoor, this.transform.position, this.transform.rotation);
        spawnedDood.transform.parent = this.transform;
    }

    private void SpawnBoss()
    {
        int bossToSpawn = Random.Range(0, bosses.Length);
        Instantiate(bosses[bossToSpawn], transform.position, transform.rotation);
    }

    private void SpawnBunchOfPowerUps()
    {
        GameObject spawnedTreat = Instantiate(bunchOfPowerUps, this.transform.position, this.transform.rotation);
        spawnedTreat.transform.parent = this.transform;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMoved();
        }
    }

    private void PlayerMoved()
    {
        mainCamera.cameraPos = new Vector3(this.transform.position.x, this.transform.position.y, mainCamera.cameraPos.z);

        if (enemiesSpawend) return;

        if (!isBoss)
            SpawnEnemies();
        else
            SpawnBoss();

        enemiesSpawend = true;
        level.CheckEnemies();
    }
}
