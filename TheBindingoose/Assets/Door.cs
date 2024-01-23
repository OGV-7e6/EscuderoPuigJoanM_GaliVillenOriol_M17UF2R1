using System;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private GameObject _exit;
    private ButtonDoor buttonDoor;
    private bool _buttonFound = false;
    // Start is called before the first frame update
    void Update()
    {
        if (GameObject.Find("ButtonDoor") != null && _buttonFound == false)
        {
            buttonDoor = GameObject.Find("ButtonDoor").GetComponent<ButtonDoor>();
            buttonDoor.OnPress += Abrir;
            _buttonFound = true;
        }
    }

    private void Abrir(object sender, EventArgs e)
    {
        _exit.SetActive(true);
        buttonDoor.OnPress -= Abrir;
    }
}