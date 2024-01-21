using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{
    [SerializeField] GameObject fireballPrefab;
    private void Update()
    {
        if (CheckIfTargetIsOnRange())
        {
            anim.SetBool("IsAttacking", true);
            anim.SetFloat("Direction", TargetDirection());
        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }
    }

    private void ShootFireBall()
    {
        GameObject fireball = Instantiate(fireballPrefab);
        fireball.transform.position = transform.position;
        fireball.transform.rotation = Quaternion.Euler(0, 0, TargetDirection() - 90f);
    }
}
