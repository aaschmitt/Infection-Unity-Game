using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

/* Specify required components */
[RequireComponent(typeof(Player))]

public class PlayerController : MonoBehaviour
{
    /* Public Properties */
    public Player Player { get; set; }
    public bool IsDashing { get; private set; }                         // Bool to determine whether or not player is currently dashing
    public bool IsMoving { get; private set;}                           // Bool to determine whether or not player is currently moving
    public Direction CurrentDirection { get; private set; }             // Reference to current direction
    public enum Direction                                               // enum defining possible directions (8 directions)
    {
        North,
        South,
        East,
        West,
        NorthWest,
        NorthEast,
        SouthWest,
        SouthEast
    }

    /* Serialized Field Private Fields */
    [SerializeField] private float speed = 5.0f;                        // Regular movement speed of player
    [SerializeField] private float dashSpeedMultiplier = 0f;            // Speed of dash (multiplied by speed)
    [SerializeField] private float dashTime = 0f;                       // How long the dash will last in seconds
    [SerializeField] private float diagonalDashMultiplier = 0f;         // Multiply diagonal dashes by this value (less than 1)
    [SerializeField] private float attackCooldown = 1f;

    /* Private Fields */
    private Rigidbody2D _rigidbody2D = null;                            // Reference to RigidBody2D component
    private bool _canAttack = true;

    void Start()
    {
        InitializeVariables();
    }
    
    void Update()
    {
        HandleInput();
    }
    
    /* Reads input and moves Player accordingly */
    private void HandleInput()
    {
        /* Cannot move while dashing */
        if (IsDashing) return;
        
        /* Move Up */
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(speed * Time.deltaTime * Vector3.up);
            
            CurrentDirection = Direction.North;
            if (Input.GetKey(KeyCode.A)) CurrentDirection = Direction.NorthWest;
            if (Input.GetKey(KeyCode.D)) CurrentDirection = Direction.NorthEast;

            IsMoving = true;
        }
        
        /* Move Down */
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(speed * Time.deltaTime * Vector3.down);

            CurrentDirection = Direction.South;
            if (Input.GetKey(KeyCode.A)) CurrentDirection = Direction.SouthWest;
            if (Input.GetKey(KeyCode.D)) CurrentDirection = Direction.SouthEast;
            
            IsMoving = true;
        }
        
        /* Move Left */
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(speed * Time.deltaTime * Vector3.left);
            
            CurrentDirection = Direction.West;
            if (Input.GetKey(KeyCode.W)) CurrentDirection = Direction.NorthWest;
            if (Input.GetKey(KeyCode.S)) CurrentDirection = Direction.SouthWest;
            
            IsMoving = true;
        }
        
        /* Move Right */
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime * Vector3.right);
            
            CurrentDirection = Direction.East;
            if (Input.GetKey(KeyCode.W)) CurrentDirection = Direction.NorthEast;
            if (Input.GetKey(KeyCode.S)) CurrentDirection = Direction.SouthEast;
            
            IsMoving = true;
        }
        
        /* Dash */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsMoving) return;
            StartCoroutine(Dash());
        }
        
        /* Use weapon */
        if (Input.GetMouseButtonDown(0) && _canAttack)
        {
            Player.Weapon.StartUsing();
            _canAttack = false;
            StartCoroutine(AttackCooldown());
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            Player.Weapon.StopUsing();
        }

        if (!Input.anyKey)
        {
            IsMoving = false;
        }
    }

    /* Coroutine to set the player's velocity for a specified time */
    private IEnumerator Dash()
    {
        Vector2 direction;
        switch (CurrentDirection)
        {
            case Direction.North:
                direction = Vector2.up;
                break;
            case Direction.West:
                direction = Vector2.left;
                break;
            case Direction.East:
                direction = Vector2.right;
                break;
            case Direction.South:
                direction = Vector2.down;
                break;
            case Direction.NorthEast:
                direction = (Vector2.up + Vector2.right) * diagonalDashMultiplier;
                break;
            case Direction.NorthWest:
                direction = (Vector2.up + Vector2.left) * diagonalDashMultiplier;;
                break;
            case Direction.SouthEast:
                direction = (Vector2.down + Vector2.right) * diagonalDashMultiplier;;
                break;
            case Direction.SouthWest:
                direction = (Vector2.down + Vector2.left) * diagonalDashMultiplier;;
                break;
            default:
                direction = Vector2.right;
                break;
        }                                                                                            // Accomodate for player's current direction
        
        IsDashing = true;                                                                                                          // Player is currently dashing
        _rigidbody2D.velocity = speed * dashSpeedMultiplier * direction;
        yield return new WaitForSeconds(dashTime);                                                                                 // Dash lasts for dashTime seconds
        _rigidbody2D.velocity = Vector2.zero;                                                                                      // Finish dash (set velocity to zero)
        IsDashing = false;                                                                                                        // Player is no longer dashing
    }
    
    /* Coroutine to prevent spam clicking */
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        _canAttack = true;
    }

    /* Initialize any necessary variables */
    private void InitializeVariables()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Player = GetComponent<Player>();
    }
}
