using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb; 
    
    // Move Vals
    private float movementX;
    private float movementY;
    
    // Private Vals
    private int count;
    private int count_num;
    
    // Public Values
    public float speed = 0; 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject PickUpParent;
    PlayerSoudManager soundManager;
    FX_Controller fx_controller;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent <Rigidbody>(); 
        soundManager = GetComponent<PlayerSoudManager>();
        fx_controller = GetComponent<FX_Controller>();
        count = 0; 
        count_num = PickUpParent.transform.childCount;
        Debug.Log($"There are {count_num} points to be picked up");
        SetCountText();
        winTextObject.SetActive(false);
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
            OnCollect();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnEnemyHit(collision);
        } 
        else if (collision.gameObject.CompareTag("Wall"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    
    void SetCountText() 
    {
        countText.text =  "Count: " + count.ToString();
        if (count >= count_num)
        {
            OnWin();
        }
    }

    void OnWin()
    {
        winTextObject.SetActive(true);
        soundManager.PlayWinSound();
        fx_controller.center_player(gameObject.transform);
        fx_controller.play_win();
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    }

    void OnCollect()
    {
        fx_controller.center_player(gameObject.transform);
        fx_controller.play_collect();
        soundManager.PlayPickUpSound();
        count = count + 1;
        SetCountText();
    }
    
    void OnEnemyHit(Collision enemy)
    {
        // Destroy the current object
        Destroy(gameObject); 
        // Update the winText to display "You Lose!"
        winTextObject.gameObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        enemy.gameObject.GetComponent<AudioSource>().Play();
        // Play FX
        fx_controller.center_player(gameObject.transform);
        fx_controller.play_death();
    }
}
