using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] private int _maxRooms;

    int roomWidth = 13;
    int roomHeight = 10;

    int gridSizeX = 20;
    int gridSizeY = 20;

    private List<GameObject> roomObjects = new List<GameObject>();

    private Queue<Vector2Int> roomQueue;

    private int[,] roomGrid;

    private int roomCount;

    private bool generationComplete;

    private void Start()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2,gridSizeY / 2 );
        TryGenerateRoom(initialRoomIndex);
    }

    private void Update()
    {
        if (roomQueue.Count > 0 && roomCount < _maxRooms && !generationComplete)
        {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            //Left
            TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
            //Right
            TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
            //Up
            TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
            //Down
            TryGenerateRoom(new Vector2Int(gridX, gridY - 1));
        }
        else if (roomCount < _maxRooms)
        {
            Debug.LogWarning("RoomCount is below the minimum. Trying again.");
            RegenerateRooms();
        }
        else if (!generationComplete)
        {
            Debug.Log($"Generation complete, {roomCount} rooms");
            generationComplete = true;
        }
    }


    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        //
        if (roomCount >= _maxRooms) return false;
        if (Random.value < 0.5f && roomIndex != Vector2Int.zero) return false;

        //evita agolpamiento entre cuartos
        if (CountAdjacentRooms(roomIndex) > 1) return false;

        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;

        var newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        newRoom.name = $"Room-{roomCount}";

        if (roomCount == 1)
        {
            //CUARTO INICIAL
            newRoom.GetComponent<Room>().RoomType = 0;
        }
        else if(roomCount == _maxRooms)
        {
            //CUARTO LLAVE
            newRoom.GetComponent<Room>().RoomType = 6;
        }
        else if (roomCount == 6)
        {
            //TIENDA
            newRoom.GetComponent<Room>().RoomType = 5;
        }
        else
        {
            //CUARTOS CON ENEMIGOS
            newRoom.GetComponent<Room>().RoomType = Random.Range(1, 5);
        }
        roomObjects.Add(newRoom);

        OpenDoors(newRoom, x, y);

        return true;
    }

    //Clear rooms and generate again
    private void RegenerateRooms()
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        TryGenerateRoom(initialRoomIndex);
    }

    void OpenDoors(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();

        //Neighbour rooms
        Room leftRoomScript = GetRoomScriprtAt(new Vector2Int(x - 1,y));
        Room rightRoomScript = GetRoomScriprtAt(new Vector2Int(x + 1, y));
        Room bottomRoomScript = GetRoomScriprtAt(new Vector2Int(x, y - 1));
        Room topRoomScript = GetRoomScriprtAt(new Vector2Int(x, y + 1));

        //Opening the doors based on the neighbours

        //Cheks Left neighbour
        if (x > 0 && roomGrid[x - 1, y] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomScript.OpenDoor(Vector2Int.right);
        }

        //Cheks Right neighbour
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomScript.OpenDoor(Vector2Int.left);
        }

        //Cheks Bottom neighbour
        if (y > 0 && roomGrid[x, y - 1] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
        }

        //Cheks Top neighbour
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);
        }
    }

    Room GetRoomScriprtAt(Vector2Int index)
    {
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);

        if (roomObject != null) return roomObject.GetComponent<Room>();
        else return null;
    }

    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        //Cheks Left room
        if (x > 0 && roomGrid[x - 1, y] != 0) count++;

        //Cheks Right room
        if (x < gridSizeX -1 && roomGrid[x + 1, y] != 0) count++;

        //Cheks Bottom room
        if (y > 0 && roomGrid[x, y -1] != 0) count++;

        //Cheks Top room
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) count++;

        return count;
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector3(roomWidth * (gridX - gridSizeX / 2), roomHeight * (gridY - gridSizeY / 2));
    }

    private void OnDrawGizmos()
    {
        Color gizmoCollor = new Color(0, 1, 1, 0.05f);
        Gizmos.color = gizmoCollor;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }
}
