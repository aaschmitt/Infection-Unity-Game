using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    /* Public fields */
    [HideInInspector] public bool IsAggravated = true;            // boolean to determine what state enemy is currently in
    [HideInInspector] public Vector3 Direction = Vector3.zero;    // Current moving direction of enemy -- used primarily for animating
    
    /* Serialized Private Fields */
    [SerializeField] private float speed = 0f;            // Speed of enemy
    [SerializeField] private Weapon weapon = null;        // Weapon this enemy is wielding
    [SerializeField] private GameObject target = null;    // Target this enemy will approach and attack
    [SerializeField] private float distance = 0f;         // Once enemy is within this distance from target, stop approaching (for ranged attacks)

    void Update()        // Update method handles switching of states
    {
        if (IsAggravated)
        {
            AggravatedUpdate();
            weapon.StartUsing();
        }
        else
        {
            IdleUpdate();
        }
    }

    private void AggravatedUpdate()
    {
        ApproachTarget();
    }

    private void IdleUpdate()
    {
        
    }

    private void SwitchToIdle()
    {
        if (!IsAggravated) return;
        IsAggravated = false;

        weapon.StopUsing();
    }

    private void ApproachTarget()
    {
        if (!target)                                                               // If target is null, switch to idle state
        {
            SwitchToIdle();
            return;
        }
        
        if (Vector2.Distance(target.transform.position, transform.position) <= distance)       // Stop enemy from approaching player at specified distance
        {
            Direction = Vector3.zero;
            return;
        }

        Vector3 direction = transform.position - target.transform.position;        // Calculate direction vector
        Direction = direction;
        direction = -direction.normalized;                                         // Normalize resultant vector to unit vector
        transform.position += Time.deltaTime * speed * direction;                  // Move in the direction of the target every frame
    }
}
