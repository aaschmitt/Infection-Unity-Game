using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(LookAt))]
public class Gun : Weapon
{
    /* Serialized Private Fields */
    [SerializeField] private Bullet bullet = null;                                // The projectile of which this weapon will fire
    [SerializeField] private Transform firingPoint = null;                        // The point on the gun where the bullet will be instantiated
    [SerializeField] private int bulletsPerFire = 1;                              // Number of bullets per call to the Fire() method (default is one)
    [SerializeField] private float timeBetweenBullets = 1f;                       // Amount of time (in seconds) between each bullet per Fire()
    [SerializeField] private float timeBetweenFires = 1f;                         // Amount of time entity must wait before firing again
    
    /* Private Fields */
    private bool _inUse = false;                                                  // Bool to determine whether or not the gun is being fired
    private IEnumerator _firingGun = null;                                        // Reference to the coroutine that fires the gun
    private LookAt _lookAt = null;

    /* Initialize variables upon start */
    private void Start()
    {
        InitializeVariables();
    }

    /* Method to fire the gun -- instantiate bullets */
    private void Fire()
    {
        StartCoroutine(InstantiateBullets());
    }
    
    /* Coroutine to continuously fire bullets while attack button is held down */
    private IEnumerator FiringGun()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(timeBetweenFires);   
        }
    }

    /* Coroutine to instantiate bullets per fire */
    private IEnumerator InstantiateBullets()
    {
        for (int i = 0; i < bulletsPerFire; i++)
        {
            var bul = Instantiate(bullet, firingPoint.transform.position, Quaternion.identity);
            bul.Direction = _lookAt.Direction.normalized;
            yield return new WaitForSeconds(timeBetweenBullets);
        }
    }

    /* Implement Weapon's Using() methods */
    public override void StartUsing()
    {
        if (_inUse) return;
        StartCoroutine(_firingGun);
        _inUse = true;
    }

    public override void StopUsing()
    {
        if (!_inUse) return;
        StopCoroutine(_firingGun);
        _inUse = false;
    }

    private void InitializeVariables()
    {
        _firingGun = FiringGun();
        _lookAt = GetComponent<LookAt>();
    }
    
    /* Stops coroutines when destroyed */
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
