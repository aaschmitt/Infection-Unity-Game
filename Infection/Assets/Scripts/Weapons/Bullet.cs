using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Bullet : MonoBehaviour
{
    /* Public properties */
    public Vector3 Direction { get; set; }                               // Direction this bullet will travel
    public bool IsPlayerBullet { get; set; }                             // Bool to determine whether bullet should damage the player object or not
    
    /* Serialized Private Fields */
    [SerializeField] private float speed = 1f;                            // Speed this bullet will travel
    [SerializeField] private float damage = 1f;                           // Amount of damage this bullet will inflict upon collision
    [SerializeField] private float lifetime = 1f;

    /* Start bullet's lifetime coroutine upon start */
    void Start()
    {
        StartCoroutine(DestroyBulletAfterSeconds());
    }
    
    /* Move bullet in direction every frame */
    void Update()
    {
        MoveBullet();
    }
    
    /* Coroutine to destroy projectile gameobject */
    private IEnumerator DestroyBulletAfterSeconds()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    /* Move the bullet in the specified direction */
    private void MoveBullet()
    {
        transform.position += Time.deltaTime * speed * Direction;         // Move projectile towards target
    }

    /* Stop any coroutines upon destruction of bullet */
    void OnDestroy()
    {
        StopAllCoroutines();
    }
    
    /* Damage an entity upon collision */
    private void DamageEntityOnCollision(GameObject entity)
    {
        Entity e = entity.GetComponent<Entity>();
        if (e != null)
        {
            if (e.gameObject.CompareTag("Player") && IsPlayerBullet) return;
            e.Damage(damage);
            Destroy(gameObject);
        }
    }
    
    /* Handle collisions */
    private void OnCollisionEnter2D(Collision2D other)
    {
        DamageEntityOnCollision(other.gameObject);
    }
}
