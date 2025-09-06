using UnityEngine;

public class SimpleFall : MonoBehaviour
{
    private float fallSpeed = 4f;
    
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}