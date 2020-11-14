using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* Serialized Field Private Fields */
    [SerializeField] private float speed = 5.0f;
    
    void Update()
    {
        HandleInput();
    }
    
    /* Reads input and moves Player accordingly */
    private void HandleInput()
    {
        /* Move Up */
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(speed * Time.deltaTime * Vector3.up);
        }
        
        /* Move Down */
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(speed * Time.deltaTime * Vector3.down);
        }
        
        /* Move Left */
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(speed * Time.deltaTime * Vector3.left);
        }
        
        /* Move Right */
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime * Vector3.right);
        }
    }
}
