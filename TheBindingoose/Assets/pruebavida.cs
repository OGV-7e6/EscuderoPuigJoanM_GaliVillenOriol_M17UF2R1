using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class pruebavida : Character
{
    private bool isHit;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject salud;
    [SerializeField] private float vidaMax;
    [SerializeField] private GameObject ammoPickup; // Nuevo GameObject para el ammo
    [SerializeField] private AudioSource death; // Agregado: Sonido de curación

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vida = 75;
        vidaMax = vida;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("bala") || && !isHit)
    //    {
    //        Debug.Log("me dieron");
    //        vida = vida - 25;
    //        if (vida <= 0)
    //        {
    //            Destroy(this.gameObject);
    //        }
    //        else
    //        {
    //            // Calcular el nuevo color basado en la vida restante
    //            float colorIntensity = vida / 75.0f; // Ajusta según la vida máxima
    //            Color newColor = new Color(colorIntensity, 0f, 0f, 1.0f);

    //            // Aplicar el nuevo color al material del sprite
    //            spriteRenderer.color = newColor;
    //        }
    //    }
    //}
    public void TakeDamage(int damage)
    {
        vida -= damage;
        death.Play();

        if (vida <= 0)
        {
            SpawnRandomPickup(); // Llama a la función para decidir qué objeto soltar
            Destroy(this.gameObject);
        }
        else
        {
            // Calcular el nuevo color basado en la vida restante
            float colorIntensity = vida / vidaMax; // Ajusta según la vida máxima
            UnityEngine.Color newColor = new (colorIntensity, 0f, 0f, 1.0f);
            Debug.Log("Color: " + colorIntensity + " - " + vida + "/" + vidaMax);

            // Aplicar el nuevo color al material del sprite
            spriteRenderer.color = newColor;
        }
    }
    private void SpawnRandomPickup()
    {
        float randomValue = Random.value; // Obtiene un valor aleatorio entre 0 y 1

        if (randomValue < 0.4f)
        {
            // 40% de probabilidad de instanciar un medkit
            Instantiate(salud, transform.position, Quaternion.identity);
        }
        else
        {
            // 60% de probabilidad de instanciar un ammoPickup
            Instantiate(ammoPickup, transform.position, Quaternion.identity);
        }
    }
}
