using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private new Rigidbody2D rb;
    private float spd = 18;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.up * spd * Time.fixedDeltaTime);
    }
}
