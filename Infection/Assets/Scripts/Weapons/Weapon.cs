using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A weapon is responsible for instantiating projectiles in a particular pattern */
public abstract class Weapon : MonoBehaviour
{
    /* Methods to use the weapon */
    public abstract void StartUsing();
    public abstract void StopUsing();
}
