using UnityEngine;

public class PuzzleCamera : MonoBehaviour
{
    public GameObject player;
    public float rotationSpeed = 3.0f;  // Speed of rotation
    public float minYAngle = -20f;      // Min vertical angle (to prevent flipping)
    public float maxYAngle = 60f;       // Max vertical angle
    public float zoomSpeed = 2f;        // Speed of zooming
    public float minZoom = 2f;          // Closest the camera can zoom in
    public float maxZoom = 10f;         // Farthest the camera can zoom out

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

            // Handle zooming
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                float distance = offset.magnitude;
                distance -= scroll * zoomSpeed;
                distance = Mathf.Clamp(distance, minZoom, maxZoom);
                offset = offset.normalized * distance;
            }

            // Convert yaw & pitch into a rotation
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

            // Apply rotation while maintaining the updated offset
            transform.position = player.transform.position + rotation * offset;

            // Make sure the camera is always looking at the player
            transform.LookAt(player.transform.position);
        }
    }
}