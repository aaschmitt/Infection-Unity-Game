using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /* Serialized Private Fields */
    [SerializeField] private Transform target = null;
    [SerializeField] private float smoothTime = 0.3F;
    [SerializeField] private Vector3 offset = Vector3.zero;
    
    /* Private Fields */
    private Vector3 _velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(offset);                                                               // Define a target position offset to the target being fallowed
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);        // Smoothly move the camera towards target position
    }
}
