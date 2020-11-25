using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A class to define behavior for the Policeman enemy -- shoots at player
 * in single-bullet increments
 */
public class Policeman : Enemy
{
    /* Serialized Private Fields */
    [SerializeField] private float timeBetweenAttacks = 0f;                   // The time (in seconds) between each shot
    [SerializeField] private Projectile projectile = null;                    // The projectile that this policeman will shoot

    /* Instantiates a projectile and aims it at the target (Player) */
    protected override void Attack(GameObject target)
    { 
        Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity);  // Instantiate a projectile and set its target to player
        proj.SetTarget(target);
    }

    /* Shoot one projectile at a time */
    protected override IEnumerator Attacking(GameObject target)
    {
        while (isAggravated)
        {
            yield return new WaitForSeconds(timeBetweenAttacks);            // Wait until attacking again
            if (isAggravated) Attack(target);                               // Make sure enemy is still aggravated when it comes time to shoot
        }
    }
}
