using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _attackRange;
    [SerializeField] protected Transform target;
    protected Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.white;
        Handles.DrawWireDisc(transform.position, transform.forward, _attackRange);
    }

    protected bool CheckIfTargetIsOnRange()
    {
        return Vector2.Distance(transform.position, target.position) <= _attackRange;
    }
}