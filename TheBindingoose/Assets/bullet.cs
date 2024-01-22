using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private new Rigidbody2D rb;
    private float spd = 10;
    [SerializeField] private float bulletRange;
    [SerializeField] private LayerMask enemys;
    private int dmg = 30;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
/*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, bulletRange, enemys);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("me golpiaron " + enemy.name);
            enemy.GetComponent<pruebavida>().TakeDamage(dmg);
            Destroy(gameObject);

        }

        rb.MovePosition(transform.position + transform.up * spd * Time.fixedDeltaTime);
    }
}
