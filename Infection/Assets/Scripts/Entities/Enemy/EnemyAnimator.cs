using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class EnemyAnimator : EntityAnimator
{
    /* Serialized private fields */
    [SerializeField] private Enemy enemy = null;                            // The enemy that is being animated
    
    /* Private fields */
    private Animator _animator = null;                                      // The animator on this enemy visual
    private bool _facingLeft = true;
    private bool _orientCoroutineIsRunning = false;                         // Bool to determine if the orient coroutine is running
    
    void Start()
    {
        InitializeVariables();
    }

    // TODO -- Refactor this to move if statements out of update and into some sort of event system
    void Update()
    {
        if (!enemy.IsAggravated)                                            // Enemy is not aggravated, and therefore will not be moving and will be facing forward
        {
            if (_orientCoroutineIsRunning)                                  // If coroutine is running, stop it and set to idle orientation
            {
                StopAllCoroutines();
                _orientCoroutineIsRunning = false;
            }
            
            _animator.SetBool(IsRunning, false);
            _animator.SetBool(FacingForward, true);
        }
        else if (!_orientCoroutineIsRunning)
        {
            StartCoroutine(OrientVisual());
            _orientCoroutineIsRunning = true;
        }
    }
    
    /* Initialize references to animator */
    private void InitializeVariables()
    {
        _animator = GetComponent<Animator>();
    }
    
    /* Flips the visual let or right and up or down determined by the direction in which the enemy is moving */
    private IEnumerator OrientVisual()
    {
        while (true)
        {
            var x = enemy.Direction.x;
            var y = enemy.Direction.y;
            if (x != 0 || y != 0)
            {
                _animator.SetBool(IsRunning, true);
            }
            else
            {
                _animator.SetBool(IsRunning, false);
            }

            if (x > 0 && !_facingLeft)                                                 // Facing left
            {
                transform.localScale = new Vector3(1, 1, 1);                  // Animate turn to left
                _facingLeft = true;
            }
            else if (x < 0)                                                            // Facing right
            {
                transform.localScale = new Vector3(-1, 1, 1);                 // Animate turn to right
                _facingLeft = false;
            }

            if (y > 0)                                                                 // Facing forward
            {
                _animator.SetBool(FacingForward, true);
            }
            else if (y < 0)                                                            // Facing backward
            {
                _animator.SetBool(FacingForward, false);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
