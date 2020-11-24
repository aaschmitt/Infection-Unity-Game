using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Approach : MonoBehaviour
{
    /* Serializable Private Fields */
    [SerializeField] private GameObject target = null;                // The gameobject to approach
    [SerializeField] private float speed = 0f;                        // Speed of approach

    void LateUpdate()
    {
        Vector3 direction = transform.position - target.transform.position;        // Calculate direction vector
        direction = -direction.normalized;                                         // Normalize resultant vector to unit vector
        transform.position +=  Time.deltaTime * speed * direction;                 // Move in the direction of the target every frame
    }
}
