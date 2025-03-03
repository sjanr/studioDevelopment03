using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 120f; // Speed of rotation

    void Update()
    {
        // Rotate the coin on the Y-axis
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
