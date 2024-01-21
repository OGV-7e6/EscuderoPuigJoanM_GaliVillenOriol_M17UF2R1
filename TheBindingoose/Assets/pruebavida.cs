using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruebavida : Character
{
    private bool isHit;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vida = 75;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bala") || other.CompareTag("Dave") && !isHit)
        {
            Debug.Log("me dieron");
            vida = vida - 25;
            if (vida <= 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                // Calcular el nuevo color basado en la vida restante
                float colorIntensity = vida / 75.0f; // Ajusta según la vida máxima
                Color newColor = new Color(colorIntensity, 0f, 0f, 1.0f);

                // Aplicar el nuevo color al material del sprite
                spriteRenderer.color = newColor;
            }
        }
    }
}
