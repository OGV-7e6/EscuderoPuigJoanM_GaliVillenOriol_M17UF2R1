using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeapon : MonoBehaviour
{
    private SpriteRenderer bulletRenderer;
    [SerializeField] private GameObject bulletPrefab;
    public Transform spawner;
    protected int armaActual;
    private PlayerInput _playerInput;

    private void Start()
    {
        bulletRenderer = GetComponent<SpriteRenderer>();
        armaActual = 0;

    }
    private void Update()
    {
        CheckFiring();
    }

    private void CheckFiring()
    {

        //Debug.Log(armaActual);

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    armaActual++;
        //    Debug.Log("aaaaaaaa");

        //}
        //else if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    armaActual++;
        //}
        //switch (armaActual)
        //{
        //    case 3:

        if (Input.GetKey(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = spawner.position;
            bullet.transform.rotation = spawner.rotation;
            Destroy(bullet, 2f);
        }
        //break;

        //}
    }
}



