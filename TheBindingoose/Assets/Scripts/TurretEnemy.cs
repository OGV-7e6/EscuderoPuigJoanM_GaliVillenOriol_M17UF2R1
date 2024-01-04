using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{
    private void Update()
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

    private void ShootFireBall()
    {
        Debug.Log("FIRE BALL!!");
    }
}
