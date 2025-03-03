using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private CinemachineCamera freeLookCamera;
    
    private Rigidbody rb;
    private bool isGrounded = true;
    private bool hasDoubleJumped = false;
    private bool isDashing = false;
    private float dashTime = 0f;

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

        // Dash logic
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && dashTime <= 0f)
        {
            StartDash();
        }

        // Update dash cooldown timer
        if (dashTime > 0f)
        {
            dashTime -= Time.deltaTime;
        }
    }

    private void MovePlayer(Vector2 direction)
    {
        if (!isDashing) // Prevent movement while dashing
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
    }

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

    private void StartDash()
    {
        isDashing = true;

        // Calculate the dash direction based on camera orientation
        Vector3 dashDirection = freeLookCamera.transform.forward;
        dashDirection.y = 0; // Keep it horizontal
        dashDirection.Normalize();

        // Apply a large impulse in the dash direction
        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);

        // Set dash timer
        dashTime = dashDuration;

        // Reset the dash after a short duration
        Invoke(nameof(EndDash), dashDuration);
    }

    private void EndDash()
    {
        isDashing = false;

        // Start the cooldown period after dash
        dashTime = dashCooldown;
    }

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
