using UnityEngine;

public class KeyHandler : MonoBehaviour
{
    public float moveSpeed = 2.0f;   // Speed of movement towards assigned position
    public float rotationSpeed = 45f; // Rotation speed
    private Vector3 targetOffset; // The position relative to the player
    private Transform player;
    private KeyManager keyManager;
    private bool unlockingGate = false;
    private Vector3 gateTarget;

    void Update()
    {
        if (unlockingGate)
        {
            // Move towards the gate
            transform.position = Vector3.Lerp(transform.position, gateTarget, Time.deltaTime * moveSpeed);

            // Despawn when close enough
            if (Vector3.Distance(transform.position, gateTarget) < 0.1f)
            {
                keyManager.RemoveKeyFromList(this);
                Destroy(gameObject);
            }
        }
        else if (player)
        {
            // Maintain relative position around the player
            Vector3 targetPosition = player.position + targetOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

            // Rotate while hovering
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    public void StartUnlockingGate(Vector3 gateLocation)
    {
        unlockingGate = true;
        gateTarget = gateLocation;
    }
    
    public void SetTargetOffset(Vector3 offset)
    {
        targetOffset = offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;

            // Disable physics after collecting
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;

            keyManager = other.GetComponent<KeyManager>();
            if (keyManager)
            {
                keyManager.AddKey(this);
            }
        }
    }
}