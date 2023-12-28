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

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        anim.SetFloat("Direction", angle);
    }

    private void ShootFireBall()
    {
        Debug.Log("FIRE BALL!!");
    }
}
