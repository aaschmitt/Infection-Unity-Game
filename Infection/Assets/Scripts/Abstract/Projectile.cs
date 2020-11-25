using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    /* Serializable protected Fields */
    [SerializeField] protected float speed = 0f;            // speed of projectile
    [SerializeField] protected float damage = 0f;           // how much damage projectile will deal upon collision
    
    /* Private Fields */
    protected GameObject target = null;                     // target that projectile is aimed towards
    
    void Update()
    {
        MoveProjectile();                                    // Move projectile every frame
    }
    
    /* Set target for projectile */
    public void SetTarget(GameObject t)
    {
        target = t;
    }
    
    /* Move projectile towards target every frame */
    protected abstract void MoveProjectile();
    
    /* Deal damage if projectile collides with an entity */
    protected void OnCollisionEnter2D(Collision2D other)
    {
        Entity entity = other.gameObject.GetComponent<Entity>();            // If collided with an entity, damage the entity and destroy the projectile
        if (entity != null)
        {
            entity.Damage(damage);
            Destroy(gameObject);
        }
    }
}
