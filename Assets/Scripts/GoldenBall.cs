using UnityEngine;

public class GoldenBall : BallController
{
    protected override void Start()
    {
        fallSpeed = 3f;
        pointValue = 5;
        ballColor = Color.yellow;
        
        base.Start();
    }

    protected override void SetupBall()
    {
        base.SetupBall();
        
        transform.localScale = Vector3.one * 1.5f;
        
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = new Color(1f, 0.843f, 0f);
            
            renderer.material.SetFloat("_Metallic", 0.8f);
            renderer.material.SetFloat("_Glossiness", 0.8f);
        }
    }

    protected override void OnCaught()
    {
        Debug.Log("Golden Ball caught! Bonus points!");
        
        if (gameManager != null && gameManager.audioSource != null && gameManager.catchSound != null)
        {
            gameManager.audioSource.pitch = 1.5f;
            gameManager.audioSource.PlayOneShot(gameManager.catchSound);
            gameManager.audioSource.pitch = 1f;
        }
    }

    // Polymorphic Movement: Golden balls fall slower and zigzag
    protected override void Move()
    {
        // Zigzag movement pattern
        float horizontalMovement = Mathf.Sin(Time.time * 2f) * 2f;
        Vector3 movement = new Vector3(horizontalMovement, -fallSpeed, 0f) * Time.deltaTime;
        transform.Translate(movement, Space.World);
        
        // Rotation for visual effect
        transform.Rotate(Vector3.up * 100f * Time.deltaTime);
    }
    
    // Override Initialize to show polymorphism
    public override void Initialize(float speed)
    {
        base.Initialize(speed * 0.6f); // Golden balls are always slower
        Debug.Log("Golden Ball initialized with reduced speed for easier catching!");
    }
}