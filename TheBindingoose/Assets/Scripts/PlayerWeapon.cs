using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
public class PlayerWeapon : MonoBehaviour
{
    private SpriteRenderer bulletRenderer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private float lastShootDate;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float shootgunCooldown;
    [SerializeField] private float kniveCooldown;
    public Transform spawner;
    protected int armaActual;
    public KeyCode[] teclasFlecha; // Puedes asignar las teclas que desees en el Inspector
    private float spd = 18; // Agrega esta línea para definir spd
    [SerializeField] private Animator animator; // Referencia al componente Animator del personaje
    private bool ataque = false;  // Nueva variable para controlar el ataque
    [SerializeField] private Transform[] Shootgun;
    [SerializeField] private float kniveRange;
    [SerializeField] private LayerMask enemys;
    [SerializeField] private AudioSource pistol;
    [SerializeField] private AudioSource shootgun;
    private int dmg = 20;



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
                    AtaqueCuchillo();



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
                    if (player.GetComponent<PlayerMovement>().ammo >=1 )
                    {
                        Disparar();
                    }
                    // Si alguna tecla de flecha está siendo presionada
                }
                break;

            case 3:
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
                    if (player.GetComponent<PlayerMovement>().ammo >= 5)
                    {
                        DispararLanzallamas();

                    }
                    // Si alguna tecla de flecha está siendo presionada
                }
                break;
        }
    }

    public bool Disparar()
    {
        if (Time.time - lastShootDate > shootCooldown)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            pistol.Play();
            lastShootDate = Time.time;
            bullet.transform.position = spawner.position;
            bullet.transform.rotation = spawner.rotation;
            Destroy(bullet, 0.9f);
            player.GetComponent<PlayerMovement>().ammo= player.GetComponent<PlayerMovement>().ammo - 1;
            player.GetComponent<PlayerMovement>().UpdateAmmoText();

            Debug.Log("balas " + player.GetComponent<PlayerMovement>().ammo);
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
                shootgun.Play();
                lastShootDate = Time.time;
                bullet.transform.position = fire.position;
                bullet.transform.rotation = fire.rotation;
                
                Destroy(bullet, 0.5f);
            }
            player.GetComponent<PlayerMovement>().ammo = player.GetComponent<PlayerMovement>().ammo - 5;
            player.GetComponent<PlayerMovement>().UpdateAmmoText();

            Debug.Log("balas " + player.GetComponent<PlayerMovement>().ammo);
            return true;

        }

        return false;
    }

    public bool AtaqueCuchillo()
    {
        if (Time.time - lastShootDate > kniveCooldown)
        {
            animator.SetBool("Ataque", true);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, kniveRange, enemys);
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("me golpiaron " + enemy.name);
                enemy.GetComponent<pruebavida>().TakeDamage(dmg);
            }
            StartCoroutine(EsperarFinAtaqueCuchillo());
            lastShootDate = Time.time;

            return true;
        }

        return false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, kniveRange);
    }

    IEnumerator EsperarFinAtaqueCuchillo()
    {
        // Espera a que termine la animación de ataque de cuchillo
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);

        // Reinicia la animación al estado de "idle"
        animator.SetBool("Ataque", false);
    }
    

}






