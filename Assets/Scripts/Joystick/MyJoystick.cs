using UnityEngine;
using UnityEngine.InputSystem;

public class MyJoystick : MonoBehaviour
{
    [SerializeField]
    private Vector2 moveVector;

    [SerializeField]
    private Vector2 centerPos;

    [SerializeField]
    private float maxLength;

    [SerializeField]
    private float currentLength;

    [SerializeField]
    RectTransform rectTransform;

    private void Start()
    {
        centerPos = Mouse.current.position.ReadValue();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            centerPos = Mouse.current.position.ReadValue();
        }

        if (Mouse.current.leftButton.isPressed)
        {
            moveVector = Mouse.current.position.ReadValue() - centerPos;

            currentLength = Mathf.Min(maxLength, moveVector.magnitude);

            moveVector = moveVector.normalized;

            transform.position = centerPos + moveVector * currentLength;
        }
    }
}
