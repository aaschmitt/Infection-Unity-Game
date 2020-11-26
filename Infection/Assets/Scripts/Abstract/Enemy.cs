﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class to define an Enemy
 * All enemies have two states -- idle and aggravated
 */
public abstract class Enemy : Entity
{
    /* Serialized Private Fields */
    [SerializeField] private float speed = 3f;                // Speed of enemy when approaching target (Player)
    
    /* Protected fields */
    protected bool isAggravated = false;                      // boolean to determine what state enemy is currently in
    protected GameObject target = null;                       // target to approach when aggravated. Will be set to player when player comes into range of sight

    /* Calls appropriate update method based on what state enemy is in */
    void Update()
    {
        if (isAggravated)
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
        ApproachTarget();                                // Approach player when aggravated
    }

    /* Defines behavior for when enemy is idle */
    private void IdleUpdate()
    {
        
    }
    
    /* Used to detect if player has entered range of sight */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isAggravated) return;                        // Enemy is already aggravated, do nothing
        
        if (other.gameObject.CompareTag("Player"))        // Player has entered enemy's range of sight
        {
            isAggravated = true;                         // Switch to aggravated state
            target = other.gameObject;                   // Set player as target
            StartCoroutine(Attacking());          // Enemy starts attacking
        }
    }
    
    /* Used to calculate target's position and approach target */
    private void ApproachTarget()
    {
        if (!target)                                                               // If target is null, switch to idle state
        {
            SwitchToIdle();
            return;
        }        
        
        Vector3 direction = transform.position - target.transform.position;        // Calculate direction vector
        direction = -direction.normalized;                                          // Normalize resultant vector to unit vector
        transform.position += Time.deltaTime * speed * direction;                   // Move in the direction of the target every frame
    }
    
    /* Stop any coroutines on destruction */
    void OnDestroy()
    {
        StopAllCoroutines();
    }
    
    /* Switches from aggravated to idle state and vice versa */
    protected void SwitchToIdle()
    {
        if (!isAggravated) return;                                    // Enemy is already in idle state, so return
        isAggravated = false;                                         // Switch to aggravated state
        StopCoroutine(Attacking());                            // Stop Attacking coroutine
    }

    /* Abstract methods to define enemy's attacking behavior (different enemies will attack differently) */
    protected abstract void Attack();
    protected abstract IEnumerator Attacking();

}
