using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 7;

    int roomWidth = 13;
    int roomHeight = 10;

    int gridSizeX = 10;
    int gridSizeY = 10;

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
        StartRoomGeneration(initialRoomIndex);
    }

    private void Update()
    {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
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
        } else if (!generationComplete)
        {
            Debug.Log($"Generation complete, {roomCount} rooms");
            generationComplete = true;
        }
    }

    private void StartRoomGeneration(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;

        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);

        return true;
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
