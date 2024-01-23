using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    //whalls and doors
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject rightDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;

    [SerializeField] GameObject topWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject leftWall;

    //floor layout
    [SerializeField] List<GameObject> roomLayouts;

    public Vector2Int RoomIndex { get; set; }
    public int RoomType { get; set; }

    public void OpenDoor(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
            topDoor.SetActive(true);
            topWall.SetActive(false);
        }

        if (direction == Vector2Int.right)
        {
            rightDoor.SetActive(true);
            rightWall.SetActive(false);
        }

        if (direction == Vector2Int.down)
        {
            bottomDoor.SetActive(true);
            bottomWall.SetActive(false);
        }

        if (direction == Vector2Int.left)
        {
            leftDoor.SetActive(true);
            leftWall.SetActive(false);
        }
    }

    private void Start()
    {
        switch (RoomType)
        {
            case 0:
                roomLayouts[0].SetActive(true);
                break;

            case 1:
                roomLayouts[1].SetActive(true);
                break;

            case 2:
                roomLayouts[2].SetActive(true);
                break;

            case 3:
                roomLayouts[3].SetActive(true);
                break;

            case 4:
                roomLayouts[4].SetActive(true);
                break;

            case 5:
                roomLayouts[5].SetActive(true);
                break;

            case 6:
                roomLayouts[6].SetActive(true);
                break;

            default:
                roomLayouts[0].SetActive(true);
                break;
        }
    }
}
