using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _attackRange;
    protected Transform target;
    protected Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("Player").transform;
    }

    /*private void OnDrawGizmosSelected()
    {
        Handles.color = Color.white;
        Handles.DrawWireDisc(transform.position, transform.forward, _attackRange);
    }*/
    protected float TargetDirection()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        return angle;
    }

    protected bool CheckIfTargetIsOnRange()
    {
        return Vector2.Distance(transform.position, target.position) <= _attackRange;
    }
}