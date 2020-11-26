using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBullet : EnemyProjectile
{
    /* Private Fields */
    private Vector3 _direction = Vector3.zero;
    
    protected override void Start()
    {
        base.Start();
        InitializeVariables();
    }
    
    /* Initialize necessary variables */
    private void InitializeVariables()
    {
        _direction = transform.position - target.transform.position;        // Calculate direction towards target
        _direction = -_direction.normalized;
    }
    
    protected override void MoveProjectile()
    {
        transform.position += Time.deltaTime * speed * _direction;         // Move projectile towards target
    }

}
