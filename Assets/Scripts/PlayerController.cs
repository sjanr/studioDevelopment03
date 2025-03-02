using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private CinemachineCamera freeLookCamera;
    private Rigidbody rb;
    void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.forward = freeLookCamera.transform.forward;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y,0);
    }

    private void MovePlayer(Vector2 direction)
{
    // Get the camera's forward direction and right direction
    Vector3 cameraForward = freeLookCamera.transform.forward;
    Vector3 cameraRight = freeLookCamera.transform.right;

    // Ignore the y-axis for the forward and right vectors (we only want horizontal movement)
    cameraForward.y = 0;
    cameraRight.y = 0;

    // Normalize the vectors
    cameraForward.Normalize();
    cameraRight.Normalize();

    // Calculate the movement direction based on the camera's orientation
    Vector3 moveDirection = cameraForward * direction.y + cameraRight * direction.x;

    // Check if the moveDirection has a valid magnitude
    if (moveDirection.sqrMagnitude > 0.01f)
    {
        // Apply the movement force (ensure we are applying the correct force mode)
        rb.AddForce(moveDirection * speed, ForceMode.Force);
    }

    // Debugging logs
    Debug.Log($"Move Direction: {moveDirection}");
    Debug.Log($"Direction: {direction}");
}



}