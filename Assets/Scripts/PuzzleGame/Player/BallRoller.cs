using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallRoller : MonoBehaviour
{
    // References
    private Rigidbody _rb;
    private LevelManager _levelManager;
    
    // Move Vals
    private float movementX;
    private float movementY;
    
    // Public Values
    public float speed = 0;
    public float resetLimit = -10f;
    public Transform cameraTransform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>(); 
        _levelManager = LevelManager.Instance;
    }
    
    // Physics Based Movement
    private void FixedUpdate() 
    {
        if (!cameraTransform) return;
        
        // Check Height
        CheckFall();

        // Get Camera Forward Direction
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        forward.Normalize();
        
        // Get Cam Dir Right
        Vector3 right = cameraTransform.right;
        right.y = 0;
        right.Normalize();
        
        // World Space
        // Convert movement direction from local to world space
        Vector3 moveDirection = (forward * movementY + right * movementX).normalized;
        
        _rb.AddForce(moveDirection * speed, ForceMode.Force);
    }
    
    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        movementX = movementVector.x; 
        movementY = movementVector.y;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void CheckFall()
    {
        if (transform.position.y < resetLimit)
        {
            _levelManager.RestartLevel();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LevelManager.Instance.RestartLevel(); 
        }
    }
}
