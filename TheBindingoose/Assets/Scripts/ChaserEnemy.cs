using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : Enemy
{
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        if (CheckIfTargetIsOnRange())
        {
            anim.SetBool("IsMoving", true);
            RotateTowardsTarget();
            MoveTowardsTarget();
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
