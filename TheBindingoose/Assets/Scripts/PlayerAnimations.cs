using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        if (hor != 0 || ver != 0)
        {
            animator.SetFloat("Horizontal",hor);
            animator.SetFloat("Vertical",ver);
            animator.SetFloat("Velocidad",1);
        }
        else
        {
            animator.SetFloat("Velocidad", 0);
        }
    }
}
