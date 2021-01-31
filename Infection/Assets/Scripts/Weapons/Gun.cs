using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(LookAt))]
public class Gun : Weapon
{
    /* Serialized Private Fields */
    [SerializeField] private GameObject visual = null;                            // the gameobject that holds the visual sprite of the gun
    [SerializeField] private Bullet bullet = null;                                // The projectile of which this weapon will fire
    [SerializeField] private Transform firingPoint = null;                        // The point on the gun where the bullet will be instantiated
    [SerializeField] private int bulletsPerFire = 1;                              // Number of bullets per call to the Fire() method (default is one)
    [SerializeField] private float timeBetweenBullets = 1f;                       // Amount of time (in seconds) between each bullet per Fire()
    [SerializeField] private float timeBetweenFires = 1f;                         // Amount of time entity must wait before firing again
    [SerializeField] private bool isWieldedByPlayer = false;
    [SerializeField] private float enemyFiringMultiplier = 2f;                    // Multiplier of timeBetweenFires for enemies
    [SerializeField] private GameObject shootParticleSystem = null;

    /* Private Fields */
    private bool _inUse = false;                                                  // Bool to determine whether or not the gun is being fired
    private IEnumerator _firingGun = null;                                        // Reference to the coroutine that fires the gun
    private LookAt _lookAt = null;
    private SpriteRenderer _visualSprite = null;

    /* Initialize variables upon start */
    private void Start()
    {
        InitializeVariables();
    }

    private void Update()
    {
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            _visualSprite.flipY = true;
        }
        else
        {
            _visualSprite.flipY = false;
        }
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
            if (isWieldedByPlayer)
            {
                Fire();
                yield return new WaitForSeconds(timeBetweenFires); 
            }
            else
            {
                yield return new WaitForSeconds(timeBetweenFires);
                Fire();   
            }
        }
    }

    /* Coroutine to instantiate bullets per fire */
    private IEnumerator InstantiateBullets()
    {
        for (int i = 0; i < bulletsPerFire; i++)
        {
            var bul = Instantiate(bullet, firingPoint.transform.position, Quaternion.identity);
            if (shootParticleSystem)
            {
                Instantiate(shootParticleSystem, firingPoint.position, Quaternion.identity);
            }
            if (!isWieldedByPlayer)
            {
                ColorBullet(bul, Color.red);
            }
            bul.Direction = _lookAt.Direction.normalized;
            if (isWieldedByPlayer) bul.IsPlayerBullet = true;
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
        _visualSprite = visual.GetComponent<SpriteRenderer>();

        if (!isWieldedByPlayer)                                    // if gun is wielded by an enemy, multiply the time between fires (slows down enemy firerate)
        {
            timeBetweenFires *= enemyFiringMultiplier;
        }
    }

    private void ColorBullet(Bullet bul, Color newColor)
    {
        var bulletSprite = bul.GetVisual().GetComponent<SpriteRenderer>();
        bulletSprite.color = newColor;
    }
    
    /* Stops coroutines when destroyed */
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
