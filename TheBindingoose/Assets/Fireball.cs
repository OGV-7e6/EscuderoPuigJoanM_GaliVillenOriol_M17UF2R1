using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] float spd;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.MovePosition(transform.position + transform.up * spd * Time.fixedDeltaTime);
    }
}
