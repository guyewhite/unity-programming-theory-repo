using UnityEngine;

public class BallController : MonoBehaviour
{
    protected float fallSpeed = 5f;
    protected int pointValue = 1;
    protected Color ballColor = Color.white;
    protected GameManager gameManager;

    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        SetupBall();
    }

    protected virtual void SetupBall()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = ballColor;
        }
    }

    protected virtual void Update()
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                gameManager.BallCaught(pointValue);
            }
            OnCaught();
            Destroy(gameObject);
        }
    }

    protected virtual void OnCaught()
    {
        // Override this in child classes for special effects
    }
}