using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movementInput;
    [SerializeField] private float _speed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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
