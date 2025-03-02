using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    void Start()
    {
        
    }

    public UnityEvent OnSpacePressed = new UnityEvent();
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        OnSpacePressed?.Invoke();
    }

    Vector2 input = Vector2.zero;

    if (Input.GetKey(KeyCode.A))
    {
        input += Vector2.left; // Move left
    }
    if (Input.GetKey(KeyCode.D))
    {
        input += Vector2.right; // Move right
    }
    if (Input.GetKey(KeyCode.W))
    {
        input += Vector2.up; // Move up (forward)
    }
    if (Input.GetKey(KeyCode.S))
    {
        input += Vector2.down; // Move down (backward)
    }
    OnMove?.Invoke(input);
}

}
