using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfTargetIsOnRange())
        {
            anim.SetBool("IsAttacking", true);
            RotateTowardsTarget();
        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }
    }
}
