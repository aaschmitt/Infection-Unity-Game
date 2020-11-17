using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    /* Serialized Field Private Fields */
    [SerializeField] private float speed = 5.0f;                        // Regular movement speed of player
    [SerializeField] private float dashSpeedMultiplier = 0f;            // Speed of dash (multiplied by speed)
    [SerializeField] private float dashTime = 0f;                       // How long the dash will last in seconds
    
    /* Private Fields */
    private Rigidbody2D _rigidbody2D = null;                            // Reference to RigidBody2D component
    private bool _isDashing = false;                                    // Bool to determine whether or not player is currently dashing
    private bool _isMoving = false;                                     // Bool to determine whether or not player is currently moving
    private Direction _currentDirection;                                // Reference to current direction

    private enum Direction                                               // enum defining possible directions (8 directions)
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
        if (_isDashing) return;
        
        /* Move Up */
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(speed * Time.deltaTime * Vector3.up);
            
            _currentDirection = Direction.North;
            if (Input.GetKey(KeyCode.A)) _currentDirection = Direction.NorthWest;
            if (Input.GetKey(KeyCode.D)) _currentDirection = Direction.NorthEast;

            _isMoving = true;
        }
        
        /* Move Down */
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(speed * Time.deltaTime * Vector3.down);

            _currentDirection = Direction.South;
            if (Input.GetKey(KeyCode.A)) _currentDirection = Direction.SouthWest;
            if (Input.GetKey(KeyCode.D)) _currentDirection = Direction.SouthEast;
            
            _isMoving = true;
        }
        
        /* Move Left */
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(speed * Time.deltaTime * Vector3.left);
            
            _currentDirection = Direction.West;
            if (Input.GetKey(KeyCode.W)) _currentDirection = Direction.NorthWest;
            if (Input.GetKey(KeyCode.S)) _currentDirection = Direction.SouthWest;
            
            _isMoving = true;
        }
        
        /* Move Right */
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime * Vector3.right);
            
            _currentDirection = Direction.East;
            if (Input.GetKey(KeyCode.W)) _currentDirection = Direction.NorthEast;
            if (Input.GetKey(KeyCode.S)) _currentDirection = Direction.SouthEast;
            
            _isMoving = true;
        }
        
        /* Dash */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_isMoving) return;
            StartCoroutine(Dash());
        }

        _isMoving = false;
    }

    /* Coroutine to set the player's velocity for a specified time */
    private IEnumerator Dash()
    {
        Vector2 direction;
        switch (_currentDirection)
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
                direction = Vector2.up + Vector2.right;
                break;
            case Direction.NorthWest:
                direction = Vector2.up + Vector2.left;
                break;
            case Direction.SouthEast:
                direction = Vector2.down + Vector2.right;
                break;
            case Direction.SouthWest:
                direction = Vector2.down + Vector2.left;
                break;
            default:
                direction = Vector2.right;
                break;
        }                                                                                            // Accomodate for player's current direction
        
        _isDashing = true;                                                                                                          // Player is currently dashing
        _rigidbody2D.velocity = speed * dashSpeedMultiplier * direction;
        yield return new WaitForSeconds(dashTime);                                                                                 // Dash lasts for dashTime seconds
        _rigidbody2D.velocity = Vector2.zero;                                                                                      // Finish dash (set velocity to zero)
        _isDashing = false;                                                                                                        // Player is no longer dashing
    }
    
    /* Initialize any necessary variables */
    private void InitializeVariables()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
}
