using UnityEngine;

[RequireComponent(typeof(Camera))]                                        // CameraFollow requires a Camera component attached
public class CameraFollow : MonoBehaviour
{
    /* Serialized Private Fields */
    [SerializeField] private Transform target = null;                      // Target to follow (most usually the player)
    [SerializeField] private float smoothTime = 0.3F;                      // Time it takes camera to adjust to target (smaller time = quicker snap) (0 - 1)
    [SerializeField] private Vector3 offset = Vector3.zero;                // Offset of camera relative to target
    [SerializeField] private Vector2 bounds = Vector2.zero;                // Camera is not to exceed these bounds
    
    /* Private Fields */
    private Vector3 _velocity = Vector3.zero;
    private Vector2 _screenDimensions = Vector2.zero;                      // Vector2 to hold the screen height and width (will be calculated in world units)
    private Camera _camera = null;                                         // Holds reference to attached Camera component

    void Start()
    {
        InitializeVariables();
    }
    
    void LateUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(offset);                                                                         // Define a target position offset to the target being fallowed
        var desiredPos = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);                // Smoothly move the camera towards target position
        
        desiredPos.x = Mathf.Clamp(desiredPos.x, -1 * bounds.x + _screenDimensions.x, bounds.x - _screenDimensions.x);    // Clamp screen position so that it does not leave bounds
        desiredPos.y = Mathf.Clamp(desiredPos.y, -1 * bounds.y + _screenDimensions.y, bounds.y - _screenDimensions.y);

        transform.position = desiredPos;
    }

    /* Initialize fields & calculate screen size */
    void InitializeVariables()
    {
        _camera = GetComponent<Camera>();                                    // Get reference to camera
        _screenDimensions.y = _camera.orthographicSize;                      // Calculate camera's height and width (divided by 2) in world units
        _screenDimensions.x = _camera.aspect * _screenDimensions.y;          // Use aspect ratio to calculate width since it's variable (not fixed)
    }

    /* Draw outline of bounds in inspector */
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, bounds * 2);
    }
}
