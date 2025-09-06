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
        Initialize();
    }

    protected virtual void SetupBall()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = ballColor;
        }
    }

    // Method Overloading Example 1: Initialize with different parameters
    public virtual void Initialize()
    {
        Debug.Log($"Ball initialized with default settings");
    }
    
    public virtual void Initialize(float speed)
    {
        fallSpeed = speed;
        Debug.Log($"Ball initialized with speed: {speed}");
    }
    
    public virtual void Initialize(float speed, int points)
    {
        fallSpeed = speed;
        pointValue = points;
        Debug.Log($"Ball initialized with speed: {speed}, points: {points}");
    }
    
    public virtual void Initialize(Vector3 startVelocity, Color color)
    {
        fallSpeed = startVelocity.magnitude;
        ballColor = color;
        SetupBall();
        Debug.Log($"Ball initialized with velocity and color");
    }

    protected virtual void Update()
    {
        Move();
        
        if (transform.position.y < -10f)
        {
            if (gameManager != null)
            {
                gameManager.BallMissed();
            }
            Destroy(gameObject);
        }
    }

    // Polymorphic Movement - Different balls move differently
    protected virtual void Move()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
    
    // Method Overloading Example 2: Apply force in different ways
    public virtual void ApplyForce(float force)
    {
        fallSpeed += force;
    }
    
    public virtual void ApplyForce(Vector3 forceVector)
    {
        transform.position += forceVector * Time.deltaTime;
    }
    
    public virtual void ApplyForce(float force, Vector3 direction)
    {
        transform.position += direction.normalized * force * Time.deltaTime;
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