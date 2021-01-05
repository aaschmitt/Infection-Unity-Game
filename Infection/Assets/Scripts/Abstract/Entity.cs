using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Abstract class to define the default implementation of an entity
 * An entity is defined as some destructible object in the game with a set amount of health,
 * one that can be damaged and killed (ex. Player or Enemy)
 */
public class Entity : MonoBehaviour, IKillable, IDamageable<float>
{
    /* Serialized Private Fields */
    [SerializeField] private float health = 50f;

    /* Default implementation -- Destroy gameobject when killed */
    public void Kill()
    {
        Destroy(gameObject);
    }

    /* Default implementation -- subtract damagetaken from health */
    public void Damage(float damageTaken)
    {
        health -= damageTaken;
        if (health <= 0) Kill();
    }
}
