using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.VFX;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : Character
{
    private Vector2 _movementInput;
    [SerializeField] private float _speed;
    private PlayerInput _playerInput;
    private Transform objetoVacio;
    public SpriteRenderer playerRenderer;
    private bool isHit;
    private GameObject dave;
    private Animator animatordave;
    protected int animacion;
    protected int armaActual = 0;
    [SerializeField] private Image life;
    [SerializeField] private float vidaMax;
    public int ammo = 5;
    [SerializeField] private TMP_Text municion;
    [SerializeField] private AudioSource deathSound; // Agregado: Sonido de muerte
    [SerializeField] private AudioSource healSound; // Agregado: Sonido de curación

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        objetoVacio = transform.Find("Move it Dave"); //
        dave = GameObject.Find("Dave");
        animatordave = dave.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerInput.actions["Move"].performed += ctx => Move(ctx);
        _playerInput.actions["Move"].canceled += ctx => Move(ctx);
        _playerInput.actions["Look"].performed += ctx => Look(ctx);
        _playerInput.actions["Look"].canceled += ctx => Look(ctx);
        _playerInput.actions["SwitchWun"].performed += ctx => CambiarArma(ctx);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        originalColor = playerRenderer.color;
        vida = 100;
        animacion = 0;

    }
    void Update()
    {
        UpdateAmmoText();
        life.fillAmount = vida / vidaMax;
    }


    public void Move(InputAction.CallbackContext ctx)
    {
        float hor = ctx.ReadValue<Vector2>().x;
        float ver = ctx.ReadValue<Vector2>().y;

        if (hor != 0 || ver != 0)
        {
            animator.SetFloat("Horizontal", hor);
            animator.SetFloat("Vertical", ver);
            animator.SetFloat("Velocidad", 1);
        }
        else
        {
            animator.SetFloat("Velocidad", 0);
        }

        // Actualiza la direcci�n de movimiento para el movimiento del jugador
        _movementInput = new Vector2(hor, ver);
    }

    public void Look(InputAction.CallbackContext ctx)
    {
        if (ctx.control.device is Keyboard || ctx.control.device is Gamepad)
        {
            Vector2 input = ctx.ReadValue<Vector2>();

            if (input != Vector2.zero)
            {
                // Calcula el �ngulo de rotaci�n directamente
                float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;

                // Ajusta el �ngulo a 0, 90, 180 o 270 grados
                targetAngle = Mathf.Round(targetAngle / 90) * -90;

                // Aplica la rotaci�n al objeto vac�o
                objetoVacio.rotation = Quaternion.Euler(0, 0, targetAngle);
            }
        }
    }

    public void CambiarArma(InputAction.CallbackContext ctx)
    {
        Debug.Log("1");
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Presionaste la tecla E");
            animacion++;
            armaActual = animacion;
            if (animacion > 3)
            {
                animacion = 1;
            }
            animatordave.SetInteger("Animacion", animacion);


        }
        // Verifica si se presiona la tecla "Q"
        else if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Debug.Log("Presionaste la tecla Q");
            animacion--;
            armaActual = animacion;

            if (animacion < 1)
            {
                animacion = 3;
            }
            animatordave.SetInteger("Animacion", animacion);

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

       
        if (other.CompareTag("Enemy") && !gameObject.CompareTag("Dave") && !isHit)
        {
            vida = vida - 25;

            if (vida <= 0)
            {
                deathSound.Play();

                animator.SetBool("Death", true);
                StartCoroutine(Muerte(1f));

            }
            else
            {
                Debug.Log("daño resibio");
                // Cambia el color a rojo
                playerRenderer.color = Color.red;
                isHit = true;
                StartCoroutine(ResetColorAfterDelay(0.5f));
            }
        }
        if (other.CompareTag("salud"))
        {
            healSound.Play();
            vida = vida + 15;
            playerRenderer.color = Color.green;
            isHit = true;
            StartCoroutine(ResetColorAfterDelay(0.2f));
        }
        if (other.CompareTag("ammo"))
        {
            ammo += 15;
            UpdateAmmoText();
            playerRenderer.color = Color.yellow;
            isHit = true;
            StartCoroutine(ResetColorAfterDelay(0.15f));
        }

    }

    IEnumerator ResetColorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Restaura el color original
        playerRenderer.color = originalColor;

        isHit = false;
    }
    IEnumerator Muerte(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Restaura el color original
        CambiarAEscenaMuerte();
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movementInput * _speed;
    }

    public void UpdateAmmoText()
    {
        if (municion != null)
        {
            // Actualiza el texto con la cantidad actual de munición
            municion.text = "Munición : " + ammo;
        }
    }
    public void CambiarAEscenaMuerte()
    {
        SceneManager.LoadScene("Death");
    }
}



