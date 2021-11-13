using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{

    public int maxRooms;
    [HideInInspector]
    public int targetRooms;
    private int numberOfRoomsCreated;
    private List<Vector2Int> createdRoomGridPoints = new List<Vector2Int>();

    private int[,] grid;
    public int gridSize;
    

    public GameObject roomObj;
    // Start is called before the first frame update
    void Start()
    {
        //make sure the grid is odd so that there is always a center
        gridSize = (gridSize % 2 == 0) ? gridSize - 1 : gridSize;
        grid = new int[gridSize, gridSize];
        
        CreateLevelPlan();
        ConstructLevel();
    }

    // --- Create Level Plan (Start) ---

    private void CreateLevelPlan()
    {
        targetRooms = Random.Range(Mathf.RoundToInt(maxRooms / 2), maxRooms);
        int centerOfGrid = Mathf.FloorToInt(gridSize / 2f);
        Vector2Int centerOfGridPoint = new Vector2Int(centerOfGrid, centerOfGrid);
        Vector2Int lastRoom = Vector2Int.zero;

        MarkRoom(centerOfGridPoint);
        
        for (int i = 0; i < createdRoomGridPoints.Count; i++)
        {
            ExpandFromPoint(createdRoomGridPoints[i]);
            lastRoom = createdRoomGridPoints[i];
            createdRoomGridPoints.RemoveAt(i);
            i--;
        }

        //Make the last room the boss room
        grid[lastRoom.x, lastRoom.y] = 2;

        printGrid();
    }

    private void MarkRoom(Vector2Int gridPoint)
    {
        createdRoomGridPoints.Add(gridPoint);
        grid[gridPoint.x, gridPoint.y] = 1;
        numberOfRoomsCreated++;
        Debug.Log("placing point: " + gridPoint + " number Of created rooms = " + numberOfRoomsCreated + "/" + targetRooms);
    }

    private void ExpandFromPoint(Vector2Int gridPoint)
    {
        Vector2Int[] expandDirections = { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right };

        for (int i = 0; i < expandDirections.Length; i++)
        {
            Vector2Int currentPoint = gridPoint + expandDirections[i];

            if (Random.Range(0, 10) < Random.Range(0, 10)) continue;
            if (!CheckValidGridPoint(currentPoint)) continue;
            if (grid[currentPoint.x, currentPoint.y] == 1) continue;
            if (numberOfRoomsCreated > targetRooms) continue;

            MarkRoom(currentPoint);
        }
    }
    
    //Return true if grid point is with in grid
    private bool CheckValidGridPoint(Vector2Int gridPoint)
    {
        return (gridPoint.y >= 0 && gridPoint.y < gridSize && gridPoint.x >= 0 && gridPoint.x < gridSize);
    }

    //Testing print the created level grid
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

    // --- Create Level Plan (End) ---

    // --- Construct Level (Start) ---

    private void ConstructLevel()
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if(grid[i, j] > 0)
                {
                    CreateRoom(new Vector2Int(i, j));
                }
            }
        }
    }

    private void CreateRoom(Vector2Int gridPoint)
    {
        Vector2Int worldPoint = GridToWorldPos(gridPoint);
        string doorCode = CreateDoorDirectionCode(gridPoint);
        GameObject createdRoom = Instantiate(roomObj, new Vector3(worldPoint.x, worldPoint.y, 0), transform.rotation);
        createdRoom.transform.parent = this.gameObject.transform;
        createdRoom.GetComponent<Room>().InitiliseValue(doorCode, (grid[gridPoint.x, gridPoint.y] == 2), (worldPoint == Vector2Int.zero));
    }

    private Vector2Int GridToWorldPos(Vector2Int gridPoint)
    {
        int centerOfGrid = Mathf.FloorToInt(gridSize / 2f);
        Vector2Int centerOfGridPoint = new Vector2Int(centerOfGrid, centerOfGrid);
        Vector2Int centerAdjustedPoint = gridPoint - centerOfGridPoint;
        return (centerAdjustedPoint * 20);
    }

    //Calculates if a point has any connection points
    // string "UDLR" means that there is a door UP, DOWN, LEFT + RIGHT of room
    private string CreateDoorDirectionCode(Vector2Int gridPoint)
    {
        string doorCode = "";
        Vector2Int[] expandDirections = { Vector2Int.down, Vector2Int.up, Vector2Int.left, Vector2Int.right };
        char[] directionCodes = { 'D', 'U', 'L', 'R' };

        for (int i = 0; i < expandDirections.Length; i++)
        {
            Vector2Int currentPoint = gridPoint + expandDirections[i];

            if (!CheckValidGridPoint(currentPoint)) continue;
            if (grid[currentPoint.x, currentPoint.y] == 0) continue;

            doorCode += directionCodes[i];
        }

        return doorCode;
    }

    // --- Construct Level (End) ---
}
