using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    /* Public Properties */
    public Vector3 Direction { get; set; }                                            // public property to indicate the direction this object is looking
    
    /* Serialized Private fields */
    [SerializeField] private ObjectToLookAt lookAt = ObjectToLookAt.GameObject;       // Enum to specify what the object will look at
    [SerializeField] private GameObject target = null;                                // Target only needs to be set if not looking at mouse (defaults to player)
    
    /* Determines what this object will look at */
    private enum ObjectToLookAt
    {
        Mouse,
        GameObject,
        None
    }

    private void Start()
    {
        InitializeVariables();
    }

    /* Depending on what the target is, orient object to "look at" that target -- Defaults to orient left */
    void Update()
    {
        if (!target)
        {
            lookAt = ObjectToLookAt.None;
        }
        
        switch (lookAt)
        {
            case ObjectToLookAt.Mouse:
                Direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                break;
            
            case ObjectToLookAt.GameObject:
                Direction = target.transform.position - transform.position;
                break;

            default:
                Direction = Vector3.left;
                break;
        }
        
        var angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void InitializeVariables()
    {
        if (!target)
        {
            target = FindObjectOfType<Player>().gameObject;
        }
    }
}
