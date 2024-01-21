using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeapon : MonoBehaviour
{
    private SpriteRenderer bulletRenderer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private float lastShootDate;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float shootgunCooldown;
    public Transform spawner;
    protected int armaActual;
    public KeyCode[] teclasFlecha; // Puedes asignar las teclas que desees en el Inspector
    private float spd = 18; // Agrega esta línea para definir spd
    [SerializeField] private Animator animator; // Referencia al componente Animator del personaje
    [SerializeField] private BoxCollider2D cuchilloCollider;
    private bool ataque = false;  // Nueva variable para controlar el ataque
    [SerializeField] private Transform[] Shootgun;


    private void Start()
    {
        bulletRenderer = GetComponent<SpriteRenderer>();
        armaActual = 0;
        cuchilloCollider = GetComponent<BoxCollider2D>();

        if (cuchilloCollider == null)
        {
            // Puedes imprimir un mensaje de error o realizar alguna acción apropiada
            Debug.LogError("No se encontró el componente Collider en " + gameObject.name);
        }
        else
        {
            // Asegúrate de desactivar el collider al inicio
            cuchilloCollider.enabled = false;
        }
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
            if (armaActual > 3)
            {
                armaActual = 1;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            armaActual--;
            if (armaActual < 1)
            {
                armaActual = 3;
            }
        }

        bool arrowKeysPressed = false;  // Declarar aquí y asignar un valor por defecto

        switch (armaActual)
        {
            case 1:
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
                    if (AtaqueCuchillo())
                    {
                        animator.SetBool("Ataque", true);
                        StartCoroutine(EsperarFinAtaqueCuchillo());
                    }
                }
                break;
            case 2:
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

            case 3:
                Debug.Log("entrando");
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
     
        if (Time.time - lastShootDate > shootgunCooldown)
        {
            foreach (Transform fire in Shootgun)
            {
                GameObject bullet = Instantiate(shootPrefab);
                lastShootDate = Time.time;
                bullet.transform.position = fire.position;
                bullet.transform.rotation = fire.rotation;

                Destroy(bullet, 3f);
            }
            return true;

        }

        return false;
    }

    public bool AtaqueCuchillo()
    {
        if (Time.time - lastShootDate > shootCooldown)
        {
            // Activa el collider para el daño durante el ataque de cuchillo
            cuchilloCollider.enabled = true;

            // Después de un tiempo, desactiva el collider para evitar daño continuo
            StartCoroutine(DesactivarColliderDespuesDeTiempo(shootCooldown / 6f));

            // Actualiza el tiempo del último ataque de cuchillo
            lastShootDate = Time.time;

            return true;
        }

        return false;
    }

    IEnumerator DesactivarColliderDespuesDeTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        // Desactiva el collider después de un tiempo para evitar daño continuo
        cuchilloCollider.enabled = false;
    }
    IEnumerator EsperarFinAtaqueCuchillo()
    {
        // Espera a que termine la animación de ataque de cuchillo
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);

        // Reinicia la animación al estado de "idle"
        animator.SetBool("Ataque", false);
    }
}






