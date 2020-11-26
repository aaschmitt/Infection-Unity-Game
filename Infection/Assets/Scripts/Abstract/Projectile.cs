using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    /* Serializable protected Fields */
    [SerializeField] protected float speed = 0f;            // speed of projectile
    [SerializeField] protected float damage = 0f;           // how much damage projectile will deal upon collision
    [SerializeField] protected float lifetime = 0f;         // How long the bullet lives in the game space before being destroyed
    
    /* Private Fields */
    protected GameObject target = null;                     // target that projectile is aimed towards
    
    /* On Start, start coroutine to destroy projectile after specified amount of time */
    protected virtual void Start()
    {
        StartCoroutine(DestroyBulletAfterSeconds());
    }
    
    /* Coroutine to destroy projectile gameobject */
    private IEnumerator DestroyBulletAfterSeconds()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    
    /* Stop any coroutines upon destruction of projectile */
    void OnDestroy()
    {
        StopAllCoroutines();
    }
    
    void Update()
    {
        MoveProjectile();                                    // Move projectile every frame
    }
    
    /* Set target for projectile */
    public void SetTarget(GameObject t)
    {
        target = t;
    }
    
    /* If collided with entity, damage it. Overridable in case only certain types of entities can be damaged */
    protected virtual void DamageEntityOnCollision(GameObject entity)
    {
        Entity e = entity.GetComponent<Entity>();
        if (e != null)
        {
            e.Damage(damage);
            Destroy(gameObject);
        }
    }
    
    /* Move projectile towards target every frame */
    protected abstract void MoveProjectile();
    
    /* Deal damage if projectile collides with an entity */
    protected void OnCollisionEnter2D(Collision2D other)
    {
        DamageEntityOnCollision(other.gameObject);
    }
}
