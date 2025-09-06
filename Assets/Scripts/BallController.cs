using UnityEngine;

public class BallController : MonoBehaviour
{
    private float fallSpeed = 5f;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        
        if (transform.position.y < -10f)
        {
            if (gameManager != null)
            {
                gameManager.BallMissed();
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                gameManager.BallCaught();
            }
            Destroy(gameObject);
        }
    }
}