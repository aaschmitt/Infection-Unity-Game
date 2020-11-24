using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    /* Serializable protected Fields */
    [SerializeField] protected float speed = 0f;
    
    /* Private Fields */
    protected GameObject target = null;
    
    void Update()
    {
        MoveProjectile();
    }
    
    /* Set target for projectile */
    public void SetTarget(GameObject t)
    {
        target = t;
    }
    
    /* Move projectile towards target every frame */
    protected abstract void MoveProjectile();
}
