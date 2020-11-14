using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    /* Serialized Private Fields */
    [SerializeField] private Vector2 levelBounds = Vector2.zero;
    
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
        viewPos.x = Mathf.Clamp(viewPos.x, levelBounds.x * -1 + _objectWidth, levelBounds.x - _objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, levelBounds.y * -1 + _objectHeight, levelBounds.y - _objectHeight);
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
        Gizmos.DrawWireCube(transform.position, levelBounds * 2);
    }
}
