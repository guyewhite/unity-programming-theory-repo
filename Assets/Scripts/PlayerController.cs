using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float movementBoundary = 8f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
        
        float clampedX = Mathf.Clamp(transform.position.x, -movementBoundary, movementBoundary);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}