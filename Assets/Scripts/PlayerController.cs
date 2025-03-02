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

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        transform.forward = freeLookCamera.transform.forward;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y,0);
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







}