using UnityEngine;

public class PuzzleCamera : MonoBehaviour
{
    public GameObject player;
    public float rotationSpeed = 3.0f;  // Speed of rotation
    public float minYAngle = -20f;      // Min vertical angle (to prevent flipping)
    public float maxYAngle = 60f;       // Max vertical angle

    private float yaw = 0f; // Horizontal rotation
    private float pitch = 10f; // Vertical rotation (default starting angle)
    private Vector3 offset; // Distance from the player

    void Start()
    {
        offset = transform.position - player.transform.position; // Store initial distance
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        if (player)
        {
            // Get mouse movement
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Adjust yaw (horizontal rotation) based on mouse X movement
            yaw += mouseX;

            // Adjust pitch (vertical rotation) based on mouse Y movement
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, minYAngle, maxYAngle); // Restrict vertical rotation

            // Convert yaw & pitch into a rotation
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

            // Apply rotation while maintaining the original distance (offset)
            transform.position = player.transform.position + rotation * offset;

            // Make sure the camera is always looking at the player
            transform.LookAt(player.transform.position);
        }
    }
}