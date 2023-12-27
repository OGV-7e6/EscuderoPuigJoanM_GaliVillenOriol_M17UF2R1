using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movementInput;
    [SerializeField] private float _speed;
    private PlayerInput _playerInput;
    private Transform objetoVacio;
    private Animator animator;

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
    }

    void Start()
    {
        animator = GetComponent<Animator>();
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



    private void FixedUpdate()
    {
        _rb.velocity = _movementInput * _speed;
    }
}



