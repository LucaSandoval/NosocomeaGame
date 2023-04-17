using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewRoomGenerator : MonoBehaviour
{
    [Header("Presets")]
    public int size; //generates a new grid of size x size. 

    private NewRoom[,] grid;

    private List<string> genWeights;

    private GameObject newRoomPrefab;

    private GameObject player;

    private NavMeshSurface nms;

    public void Awake()
    {
        newRoomPrefab = Resources.Load<GameObject>("NewRoom");
        player = GameObject.FindGameObjectWithTag("Player");

        GenerateRooms();
        PlacePlayer();
        nms = GetComponent<NavMeshSurface>();
        nms.BuildNavMesh();
    }

    public void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            DeleteRooms();
        }
    }

    public void PlacePlayer()
    {
        //Place player in start room 
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (grid[y, x].type == NewRoomType.startRoom)
                {
                    player.transform.position = grid[y, x].gameObject.transform.position;
                }
            }
        }
    }

    public void DeleteRooms()
    {
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Destroy(grid[y, x].gameObject);
                grid[y, x] = null;
            }
        }
    }

    public void GenerateRooms()
    {
        float roomSize = 18f;

        int deleteCount = size - 1;

        grid = new NewRoom[size, size];

        //Set up room type randomization
        CalcRoomWeights();

        //Generate rooms
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                GameObject newRoom = Instantiate(newRoomPrefab);
                newRoom.transform.position = new Vector3(x * roomSize, 0, y * roomSize);
                newRoom.transform.SetParent(transform);
                NewRoom script = newRoom.GetComponent<NewRoom>();
                script.type = getTypeFromWeights();

                script.y = y;
                script.x = x;
                grid[y, x] = script;
            }
        }

        //Delete some random rooms
        //for (int i = 0; i < deleteCount; i++)
        //{
        //    int randY = Random.Range(0, size);
        //    int randX = Random.Range(0, size);
        //    if (isPositionValid(randY, randX))
        //    {
        //        Destroy(grid[randY, randX].gameObject);
        //        grid[randY, randX] = null;
        //    }
        //}

        ////Update door/wall data
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                //Check this room's 4 corners and fix doors 
                grid[y, x].SetLockInfo(!isPositionValid(y - 1, x),
                    !isPositionValid(y, x + 1),
                    !isPositionValid(y, x - 1),
                    !isPositionValid(y + 1, x));

                grid[y, x].SetVisibilityInfo(!isPositionValid(y - 1, x),
                    !isPositionValid(y, x + 1),
                    !isPositionValid(y, x - 1),
                    !isPositionValid(y + 1, x));
            }
        }
    }

    private NewRoomType getTypeFromWeights()
    {
        if (genWeights.Count > 0)
        {
            int rand = Random.Range(0, genWeights.Count);
            string name = genWeights[rand];
            genWeights.Remove(name);

            return (NewRoomType)System.Enum.Parse(typeof(NewRoomType), name);
        }

        return NewRoomType.lootRoom;
    }

    private void CalcRoomWeights()
    {
        //Set up room type randomization
        genWeights = new List<string>();
        int freeRooms = size * size;
        freeRooms -= 2;
        genWeights.Add("startRoom");
        genWeights.Add("endRoom");

        float enemyRoomAmmount = 0.5f;
        float lootRoomAmmount = 0.5f;

        int enemyRooms = (int)(freeRooms * enemyRoomAmmount);
        for (int i = 0; i < enemyRooms; i++)
        {
            genWeights.Add("enemyRoom");
        }

        int lootRooms = (int)(freeRooms * lootRoomAmmount);
        for (int i = 0; i < lootRooms; i++)
        {
            genWeights.Add("lootRoom");
        }

        freeRooms -= lootRooms;
        freeRooms -= enemyRooms;

        //If there are any left over, fix it
        for (int i = 0; i < freeRooms; i++)
        {
            genWeights.Add("lootRoom");
        }
    }

    private bool isPositionValid(int y, int x)
    {
        if( (y >= 0 && y < size) && (x >= 0 && x < size) )
        {
            return grid[y, x] != null;
        }

        return false;
    }
}
