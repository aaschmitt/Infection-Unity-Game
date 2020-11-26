using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]                            // Boundaries.cs requires SpriteRenderer to calculate width and height of object being clamped
public class Boundaries : MonoBehaviour
{
    /* Serialized Private Fields */
    [SerializeField] private Vector2 bounds = Vector2.zero;          // Holds the area which the object will be bound to (drawn in inspector)

    /* Private Fields */
    private float _objectWidth = 0f;
    private float _objectHeight = 0f;

    void Start()
    {
        InitializeVariables();
    }
    
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, bounds.x * -1 + _objectWidth, bounds.y - _objectWidth);        // Clamp object to bounds
        viewPos.y = Mathf.Clamp(viewPos.y, bounds.y * -1 + _objectHeight, bounds.y - _objectHeight);
        transform.position = viewPos;
    }

    /* Initialize variables */
    private void InitializeVariables()
    {
        _objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;  // extents = size of width / 2
        _objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; // extents = size of height / 2
    }
    
    /* Inspector method for displaying levelBounds */
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, bounds * 2);
    }
}
