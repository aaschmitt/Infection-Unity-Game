using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A type of projectile that can only damage the player entity
 */
public abstract class EnemyProjectile : Projectile
{
    /* Deal damage if projectile collides with a player */
    protected override void DamageEntityOnCollision(GameObject player)
    {
        PlayerController pc = player.GetComponent<PlayerController>();                          // If collided with a player, damage the player
        if (pc != null)
        {
            if (pc.IsDashing) return;
            pc.Player.Damage(damage);                                                           // Damage the Player component
            Destroy(gameObject);
        }
    }
}
