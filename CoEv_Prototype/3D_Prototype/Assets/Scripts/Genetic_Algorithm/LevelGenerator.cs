using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> standardRooms = new List<GameObject>(6);
    public List<GameObject> secrets = new List<GameObject>(3);
    public GameObject start;
    public GameObject shop;
    public GameObject boss;
    public GameObject doorFillVertical;
    public GameObject doorFillHorizontal;

    public int roomsAmount = 10; // Not including start room
    private int gridSize = 9; // 5 x 5
    private int length = 20;
    private int xOrigin = 10;
    private int zOrigin = 10;


    void instantiateStandard(bool[,] grid)
    {
        for (int i = 0; i < roomsAmount; i++)
        {
            int xGrid = Random.Range(1, gridSize + 1);
            int yGrid = Random.Range(1, gridSize + 1);

            while (((grid[xGrid + 1, yGrid] == false) && (grid[xGrid - 1, yGrid] == false) &&
                    (grid[xGrid, yGrid + 1] == false) && (grid[xGrid, yGrid - 1] == false)) || grid[xGrid, yGrid])
            {
                xGrid = Random.Range(1, gridSize + 1);
                yGrid = Random.Range(1, gridSize + 1);
            }

            var pos = new Vector3((xOrigin + ((xGrid - 1) * length)), 0, (zOrigin + ((yGrid - 1) * length)));
            int roomCode = Random.Range(0, 5);
            Instantiate(standardRooms[roomCode], pos, Quaternion.identity);
            grid[xGrid, yGrid] = true;
        }
    }

    void instantiateRoom(String type, bool[,] grid)
    {
        int gridX = Random.Range(1, gridSize + 1);
        int gridY = Random.Range(1, gridSize + 1);

        if (type == "start")
        {
            var startPos = new Vector3((xOrigin + ((gridX - 1) * length)), 0, (zOrigin + ((gridY - 1) * length)));
            Instantiate(start, startPos, Quaternion.identity);
            grid[gridX, gridY] = true;
            return;
        }

        while (((grid[gridX + 1, gridY] == false) && (grid[gridX - 1, gridY] == false) &&
                (grid[gridX, gridY + 1] == false) && (grid[gridX, gridY - 1] == false)) || grid[gridX, gridY])
        {
            gridX = Random.Range(1, gridSize + 1);
            gridY = Random.Range(1, gridSize + 1);
        }

        var pos = new Vector3((xOrigin + ((gridX - 1) * length)), 0, (zOrigin + ((gridY - 1) * length)));

        if (type == "shop")
        {
            Instantiate(shop, pos, Quaternion.identity);
        }
        else if (type == "boss")
        {
            Instantiate(boss, pos, Quaternion.identity);
        }
        else if (type == "secret")
        {
            int roomCode = Random.Range(0, 2);
            Instantiate(secrets[roomCode], pos, Quaternion.identity);
        }

        grid[gridX, gridY] = true;
    }

    void fillWalls(bool[,] grid)
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y])
                {
                    if (grid[x + 1, y] == false) // East
                    {
                        var outsidePoint = new Vector3(( (xOrigin * 3) + ((x - 1) * length)), (float)(0 + (length * 0.05)),
                            ((y - 1) * length));
                        Instantiate(doorFillHorizontal, outsidePoint, Quaternion.identity).transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);;
                    }
                    if (grid[x - 1, y] == false) // West
                    {
                        var outsidePoint = new Vector3(xOrigin + (((x - 1) * length)), (float)(0 + (length * 0.05)),
                           ((y - 1) * length));
                        Instantiate(doorFillHorizontal, outsidePoint, Quaternion.identity);
                    } 
                    if (grid[x, y + 1] == false) // North
                    {
                        var outsidePoint = new Vector3(((xOrigin * 2) + ((x - 1) * length)), (float)(0 + (length * 0.05)),
                            (zOrigin + ((y - 1) * length)));
                        Instantiate(doorFillVertical, outsidePoint, Quaternion.identity);
                    }
                    if (grid[x, y - 1] == false) // South
                    {
                        var outsidePoint = new Vector3(((xOrigin * 2) + ((x - 1) * length)), (float)(0 + (length * 0.05)),
                            (((y - 1) * length)) - zOrigin);
                        Instantiate(doorFillVertical, outsidePoint, Quaternion.identity).transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);
                    }
                }
            }
        }
    }

    void Start()
    {
        bool[,] grid = new bool[gridSize + 2, gridSize + 2];
        instantiateRoom("start", grid);
        instantiateStandard(grid);
        instantiateRoom("shop", grid);
        instantiateRoom("secret", grid);
        instantiateRoom("boss", grid);
        fillWalls(grid);
    }
}