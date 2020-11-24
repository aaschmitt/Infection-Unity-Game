using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class to define an Enemy
 * All enemies have two states -- idle and aggravated
 */
public class Enemy : Entity
{
    /* Serialized Private Fields */
    [SerializeField] private float speed = 3f;                // Speed of enemy when approaching target (Player)
    
    /* Private Fields */
    private bool _isAggravated = false;                       // boolean to determine what state enemy is currently in
    private GameObject _target = null;                        // target to approach when aggravated. Will be set to player when player comes into range of sight

    /* Calls appropriate update method based on what state enemy is in */
    void Update()
    {
        if (_isAggravated)
        {
            AggravatedUpdate();
        }
        else
        {
            IdleUpdate();
        }
    }

    /* Defines behavior for when enemy is aggravated */
    private void AggravatedUpdate()
    {
        ApproachTarget();
    }

    /* Defines behavior for when enemy is idle */
    private void IdleUpdate()
    {
        
    }
    
    /* Used to detect if player has entered range of sight */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))        // Player has entered enemy's range of sight
        {
            _isAggravated = true;                         // Switch to aggravated state
            _target = other.gameObject;                   // Set player as target
        }
    }
    
    /* Used to calculate target's position and approach target */
    private void ApproachTarget()
    {
        Vector3 direction = transform.position - _target.transform.position;        // Calculate direction vector
        direction = -direction.normalized;                                          // Normalize resultant vector to unit vector
        transform.position += Time.deltaTime * speed * direction;                   // Move in the direction of the target every frame
    }
}
