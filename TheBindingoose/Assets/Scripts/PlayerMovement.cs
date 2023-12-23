using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movementInput;
    [SerializeField] private float _speed;
    //[SerializeField] private float rotationSpeed = 10f; // Ajusta el valor según sea necesario
    private PlayerInput _playerInput;
    //private Transform objetoVacio;
    private Animator animator;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _playerInput.actions["Move"].performed += ctx => Move(ctx);
        _playerInput.actions["Move"].canceled += ctx => Move(ctx);
        //_playerInput.actions["Look"].performed += ctx => Look(ctx);
        //_playerInput.actions["Look"].canceled += ctx => Look(ctx);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //objetoVacio = GameObject.Find("Move It Dave").transform;
    }

    //public void Look(InputAction.CallbackContext ctx)
    //{
    //    float rotationAmount = ctx.ReadValue<Vector2>().x * rotationSpeed * Time.deltaTime;
    //    objetoVacio.transform.Rotate(Vector3.up, rotationAmount);
    //}

    public void Move(InputAction.CallbackContext ctx)
    {
        //float hor = Input.GetAxisRaw("Horizontal");
        //float ver = Input.GetAxisRaw("Vertical");
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
    }


    private void FixedUpdate()
    {
        _rb.velocity = _movementInput * _speed;
    }

    private void OnMove(InputValue input)
    {
        _movementInput = input.Get<Vector2>();
    }

}
