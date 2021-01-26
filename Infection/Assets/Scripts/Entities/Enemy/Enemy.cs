using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    /* Public fields */
    public GameObject Target = null;                              // Target this enemy will approach and attack
    [HideInInspector] public bool IsAggravated = true;            // boolean to determine what state enemy is currently in
    [HideInInspector] public Vector3 Direction = Vector3.zero;    // Current moving direction of enemy -- used primarily for animating
    
    /* Serialized Private Fields */
    [SerializeField] private float speed = 0f;            // Speed of enemy
    [SerializeField] private GameObject weapon = null;    // Weapon this enemy is wielding (null if no weapon)
    [SerializeField] private float distance = 0f;         // Once enemy is within this distance from target, stop approaching (for ranged attacks)
    
    /* Private fields */
    private Weapon _weapon = null;                        // Reference to the specific weapon found under the Weapon gameobject. Null if none found
    
    private void Start()
    {
        InitializeVariables();
    }
    
    void Update()        // Update method handles switching of states
    {
        if (IsAggravated)
        {
            AggravatedUpdate();
        }
        else
        {
            IdleUpdate();
        }
    }

    private void AggravatedUpdate()
    {
        if (_weapon)
        {
            _weapon.StartUsing();
        }
        
        ApproachTarget();
    }

    /* DELETE this if no idle behavior is defined */
    private void IdleUpdate()
    {
        
    }

    private void SwitchToIdle()
    {
        if (!IsAggravated) return;
        IsAggravated = false;

        if (_weapon)
        {
            _weapon.StopUsing();
        }
    }

    private void ApproachTarget()
    {
        if (!Target)                                                               // If target is null, switch to idle state
        {
            SwitchToIdle();
            return;
        }
        
        if (Vector2.Distance(Target.transform.position, transform.position) <= distance)       // Stop enemy from approaching player at specified distance
        {
            Direction = Vector3.zero;
            return;
        }

        Vector3 direction = transform.position - Target.transform.position;        // Calculate direction vector
        Direction = direction;
        direction = -direction.normalized;                                         // Normalize resultant vector to unit vector
        transform.position += Time.deltaTime * speed * direction;                  // Move in the direction of the target every frame
    }

    public void EquipWeapon(GameObject weaponPrefab)
    {
        var equippedWeapon = Instantiate(weaponPrefab, weapon.transform.position, Quaternion.identity);
        equippedWeapon.transform.parent = weapon.transform;
    }

    private void InitializeVariables()
    {
        _weapon = weapon.GetComponentInChildren<Weapon>();
        if (Target == null)
        {
            Target = FindObjectOfType<Player>().gameObject;
        }
    }
}
