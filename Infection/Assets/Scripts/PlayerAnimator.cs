using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Direction = PlayerController.Direction;

/*
 * Class responsible for handling all animations associated with the Player
 */
public class PlayerAnimator : MonoBehaviour
{
    /* Serialized Private Fields */
    [SerializeField] private GameObject player = null;                // Reference to main player gameobject
    
    /* Private fields */
    private PlayerController _playerController = null;                // PlayerController taken from player gameobject
    private Animator _animator = null;                                // Animator component taken from this gameobject
    
    /* Private Constant Animator Parameters */
    private const string IsRunning = "IsRunning";
    private const string FacingForward = "FacingForward";

    /* Initialize any needed fields on start */
    void Start()
    {
        InitializeVariables();
    }

    void Update()
    {
        OrientPlayer();

        if (_playerController.IsMoving)
        {
            _animator.SetBool(IsRunning, true);
        }
        else
        {
            _animator.SetBool(IsRunning, false);
        }
    }
    
    /* Determine direction player is facing */
    private void OrientPlayer()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _animator.SetBool(FacingForward, false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _animator.SetBool(FacingForward, true);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    /* Initialize any necessary fields */
    private void InitializeVariables()
    {
        _playerController = player.GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }
}
