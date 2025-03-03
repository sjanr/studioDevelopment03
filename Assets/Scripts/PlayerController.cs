using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private CinemachineCamera freeLookCamera;
    private Rigidbody rb;
    private bool isGrounded = true;
    private bool hasDoubleJumped = false;

    void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnSpacePressed.AddListener(Jump);
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        transform.forward = freeLookCamera.transform.forward;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    private void MovePlayer(Vector2 direction)
    {
        Vector3 cameraForward = freeLookCamera.transform.forward;
        Vector3 cameraRight = freeLookCamera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = (cameraForward * direction.y + cameraRight * direction.x).normalized;

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Vector3 targetVelocity = moveDirection * speed;
            Vector3 velocityChange = targetVelocity - rb.linearVelocity;

            // Apply gradual force for smoother movement
            rb.AddForce(velocityChange, ForceMode.Acceleration);
        }
    }

    // Jump logic
    private void Jump()
    {
        if (isGrounded)
        {
            // Jump if grounded
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            hasDoubleJumped = false;
        }
        else if (!hasDoubleJumped)
        {
            // Double jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            hasDoubleJumped = true;
        }
    }

    // Detect ground collision with a more robust method
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            hasDoubleJumped = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
