using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A generic interface for objects that can be damaged
 */
public interface IDamageable<T>
{
    void Damage(T damageTaken);
}
