using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public int maxRooms;
    [HideInInspector]
    public int targetRooms;

    private int[,] grid;
    public int gridSize;

    public GameObject room;
    // Start is called before the first frame update
    void Start()
    {
        //make sure the grid is odd so that there is always a center
        gridSize = (gridSize % 2 == 0) ? gridSize - 1 : gridSize;
        grid = new int[gridSize, gridSize];
        
        CreateLevel();
    }

    private void printGrid()
    {
        string tableValue = "";
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            string rowValues = "";
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                rowValues += grid[i, j].ToString() + " ";
            }
            tableValue += rowValues + "\n";
        }
        Debug.Log(tableValue);
    }

    private void CreateLevel()
    {
        targetRooms = Random.Range(Mathf.FloorToInt(maxRooms / 2), maxRooms);
        int centerOfGrid = Mathf.CeilToInt(gridSize / 2f);
        grid[centerOfGrid, centerOfGrid] = 1;

        printGrid();
        //GameObject startingRoom = Instantiate(room, transform.position, transform.rotation);
    }
    public void CheckEnemies()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
            Debug.Log("Enemies Exist");
        else
            Debug.Log("All Enemies Killed");
    }
}
