using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBullet : Projectile
{
    /* Private Fields */
    private Vector3 _direction = Vector3.zero;
    
    private void Start()
    {
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
