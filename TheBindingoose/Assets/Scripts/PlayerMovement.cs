using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : Character
{
    private Vector2 _movementInput;
    [SerializeField] private float _speed;
    private PlayerInput _playerInput;
    private Transform objetoVacio;
    public SpriteRenderer playerRenderer;
    private bool isHit;

    public Sprite[] spritesArmas; // Coloca aquí tus sprites de las 4 armas en el orden mencionado

    private int armaActual = 0;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        objetoVacio = transform.Find("Move it Dave"); // Asegúrate de que el objeto vacío está anidado en el jugador
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

        // Actualiza la dirección de movimiento para el movimiento del jugador
        _movementInput = new Vector2(hor, ver);
    }

    public void Look(InputAction.CallbackContext ctx)
    {
        if (ctx.control.device is Keyboard || ctx.control.device is Gamepad)
        {
            Vector2 input = ctx.ReadValue<Vector2>();

            if (input != Vector2.zero)
            {
                // Calcula el ángulo de rotación directamente
                float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;

                // Ajusta el ángulo a 0, 90, 180 o 270 grados
                targetAngle = Mathf.Round(targetAngle / 90) * -90;

                // Aplica la rotación al objeto vacío
                objetoVacio.rotation = Quaternion.Euler(0, 0, targetAngle);
            }
        }
    }

    public void CambiarArma(InputAction.CallbackContext ctx)
    {
        Debug.Log("Cambiando arma");

        float cambio = ctx.ReadValue<float>();

        if (cambio > 0)
        {
            // Cambio a la siguiente arma
            armaActual = (armaActual + 1) % spritesArmas.Length;
        }
        else
        {
            // Cambio a la arma anterior
            armaActual = (armaActual - 1 + spritesArmas.Length) % spritesArmas.Length;
        }

        ActualizarSpriteArma();
    }

    private void ActualizarSpriteArma()
    {
        Debug.Log("act arma");

        // Actualiza el sprite del secuaz con el sprite de la arma actual
        playerRenderer.sprite = spritesArmas[armaActual];
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        vida = vida - 25;
        if (other.CompareTag("Enemy") && !isHit)
        {
            if (vida <= 0)
            {
                animator.SetBool("Death", true);
            }
            else
            {
                // Cambia el color a rojo
                playerRenderer.color = Color.red;
                isHit = true;
                StartCoroutine(ResetColorAfterDelay(0.5f));
            }
        }
    }

    IEnumerator ResetColorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Restaura el color original
        playerRenderer.color = originalColor;

        isHit = false;
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movementInput * _speed;
    }
}



