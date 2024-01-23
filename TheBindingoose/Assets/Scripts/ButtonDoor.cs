using System;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{
    [SerializeField] private Sprite _buttonPressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public event EventHandler OnPress;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPress?.Invoke(this, EventArgs.Empty);
            gameObject.GetComponent<SpriteRenderer>().sprite = _buttonPressed;
        }
    }
}
