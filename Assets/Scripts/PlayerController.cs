using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb; 
    
    // Move Vals
    private float movementX;
    private float movementY;
    
    // Public Values
    public float speed = 0; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent <Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Physics Based Movement
    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        _rb.AddForce(movement * speed); 
    }
    
    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        movementX = movementVector.x; 
        movementY = movementVector.y;
    }
    
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
        }
    }
}
