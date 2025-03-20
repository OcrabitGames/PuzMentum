using System.Collections;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallRoller : MonoBehaviour
{
    private Rigidbody _rb;
    
    // Move Vals
    private float movementX;
    private float movementY;
    
    // Public Values
    public float speed = 0;
    public Transform cameraTransform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>(); 
    }
    
    // Physics Based Movement
    private void FixedUpdate() 
    {
        if (!cameraTransform) return;
        
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
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(RestartLevel());
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator RestartLevel()
    {
        // Destroy Player
        //Destroy(gameObject);
        
        float delay = 3f;
        yield return new WaitForSeconds(delay); // Wait for specified time
        LevelManager.Instance.RestartLevel(); 
        Debug.Log("Restarting Level...");
    }
}
