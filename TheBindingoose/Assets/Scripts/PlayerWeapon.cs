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
    public KeyCode[] teclasFlecha; // Puedes asignar las teclas que desees en el Inspector
    private float spd = 18; // Agrega esta línea para definir spd



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

        bool arrowKeysPressed = false;  // Declarar aquí y asignar un valor por defecto

        switch (armaActual)
        {
            case 3:
                // Verifica si alguna de las teclas de flecha está siendo presionada
                foreach (KeyCode tecla in teclasFlecha)
                {
                    if (Input.GetKey(tecla))
                    {
                        arrowKeysPressed = true;
                        break;
                    }
                }

                if (arrowKeysPressed)
                {
                    // Si alguna tecla de flecha está siendo presionada
                    Disparar();
                }
                break;

            case 4:
                Debug.Log("entrando");
                // Verifica si alguna de las teclas de flecha está siendo presionada
                arrowKeysPressed = false;  // Reiniciar el valor a falso
                foreach (KeyCode tecla in teclasFlecha)
                {
                    if (Input.GetKey(tecla))
                    {
                        arrowKeysPressed = true;
                        break;
                    }
                }

                if (arrowKeysPressed)
                {
                    // Si alguna tecla de flecha está siendo presionada
                    DispararLanzallamas();
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
    public bool DispararLanzallamas()
    {
        if (Time.time - lastShootDate > shootCooldown)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = spawner.position;

                float spreadAngle = Random.Range(-45f, 45f);

                // Guardar la rotación original del spawner
                Quaternion originalRotation = spawner.rotation;

                // Aplicar la rotación esparcida al spawner
                spawner.rotation *= Quaternion.Euler(0f, spreadAngle, 0f);

                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

                // Separar la operación de suma para evitar la ambigüedad
                Vector2 newPosition = bulletRb.position + (Vector2)bullet.transform.up * spd * Time.fixedDeltaTime;
                bulletRb.MovePosition(newPosition);

                float angle = Mathf.Atan2(bulletRb.velocity.y, bulletRb.velocity.x) * Mathf.Rad2Deg;
                bulletRb.MoveRotation(angle);

                // Restablecer la rotación original del spawner
                spawner.rotation = originalRotation;

                Destroy(bullet, 2f);
            }

            lastShootDate = Time.time;
            return true;
        }

        return false;
    }



}






