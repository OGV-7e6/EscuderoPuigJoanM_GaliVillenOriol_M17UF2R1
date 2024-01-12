using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeapon : MonoBehaviour
{
    private SpriteRenderer bulletRenderer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float lastShootDate;
    [SerializeField] private float shootCooldown;
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            armaActual++;
            Debug.Log("aaaaaaaa");
            if (armaActual > 4)
            {
                armaActual = 1;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            armaActual--;
            if (armaActual < 1)
            {
                armaActual = 4;
            }
        }
        switch (armaActual)
        {
            case 3:

                if (Input.GetKey(KeyCode.Space))
                {
                    Disparar();
                }
                break;

        }
    }
    public bool Disparar()
    {
        if (Time.time - lastShootDate > shootCooldown)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            lastShootDate = Time.time;
            bullet.transform.position = spawner.position;
            bullet.transform.rotation = spawner.rotation;
            Destroy(bullet, 2f);
            return true;
        }

        return false;
    }
}





