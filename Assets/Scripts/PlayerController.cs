using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float movementBoundary = 8f;

    void Update()
    {
        float horizontalInput = 0f;
        
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed)
            {
                horizontalInput = -1f;
            }
            else if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed)
            {
                horizontalInput = 1f;
            }
        }
        
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
        
        float clampedX = Mathf.Clamp(transform.position.x, -movementBoundary, movementBoundary);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}