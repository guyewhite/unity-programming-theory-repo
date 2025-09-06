using UnityEngine;

public class BonusBall : BallController
{
    private float waveAmplitude = 3f;
    private float waveFrequency = 2f;
    private float baseXPosition;
    private bool isSplitting = false;

    protected override void Start()
    {
        fallSpeed = 4f;
        pointValue = 2;
        ballColor = new Color(0.5f, 1f, 0.5f); // Light green
        baseXPosition = transform.position.x;
        
        base.Start();
    }

    protected override void SetupBall()
    {
        base.SetupBall();
        
        // Pulsing scale effect
        transform.localScale = Vector3.one * 1.2f;
        
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = ballColor;
        }
    }

    // Polymorphic Movement: Wave pattern movement
    protected override void Move()
    {
        // Wave movement pattern (sine wave)
        float waveOffset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
        Vector3 newPosition = transform.position;
        newPosition.x = baseXPosition + waveOffset;
        newPosition.y -= fallSpeed * Time.deltaTime;
        transform.position = newPosition;
        
        // Pulsing effect
        float pulseScale = 1.2f + Mathf.Sin(Time.time * 5f) * 0.2f;
        transform.localScale = Vector3.one * pulseScale;
        
        // Rotation
        transform.Rotate(Vector3.forward * 150f * Time.deltaTime);
    }

    // Override all Initialize methods to show different polymorphic behavior
    public override void Initialize()
    {
        base.Initialize();
        Debug.Log("Bonus Ball: Initialized with wave movement!");
    }
    
    public override void Initialize(float speed)
    {
        base.Initialize(speed);
        waveFrequency = speed * 0.5f; // Faster balls wave faster
        Debug.Log($"Bonus Ball: Wave frequency adjusted to {waveFrequency}");
    }
    
    public override void Initialize(float speed, int points)
    {
        base.Initialize(speed, points);
        waveAmplitude = points; // More points = wider waves
        Debug.Log($"Bonus Ball: Wave amplitude set to {waveAmplitude} based on points");
    }
    
    public override void Initialize(Vector3 startVelocity, Color color)
    {
        base.Initialize(startVelocity, color);
        waveAmplitude = startVelocity.x; // X velocity determines wave amplitude
        waveFrequency = startVelocity.z; // Z velocity determines wave frequency
        Debug.Log($"Bonus Ball: Custom wave pattern from velocity vector");
    }

    // Override ApplyForce to create unique behavior
    public override void ApplyForce(float force)
    {
        // Bonus balls split when force is applied!
        if (!isSplitting && force > 2f)
        {
            SplitBall();
        }
        base.ApplyForce(force);
    }
    
    public override void ApplyForce(Vector3 forceVector)
    {
        // Change wave pattern based on force direction
        waveAmplitude += forceVector.x;
        waveFrequency += forceVector.z;
        base.ApplyForce(forceVector);
        Debug.Log($"Bonus Ball: Wave pattern modified by force vector");
    }
    
    public override void ApplyForce(float force, Vector3 direction)
    {
        // Bonus balls bounce off in opposite direction
        Vector3 bounceDirection = -direction;
        base.ApplyForce(force * 0.5f, bounceDirection);
        Debug.Log("Bonus Ball: Bounced in opposite direction!");
    }

    private void SplitBall()
    {
        if (isSplitting) return;
        isSplitting = true;
        
        Debug.Log("Bonus Ball is splitting!");
        
        // Create two smaller balls
        for (int i = 0; i < 2; i++)
        {
            GameObject splitBall = Instantiate(gameObject);
            splitBall.transform.localScale = transform.localScale * 0.7f;
            splitBall.transform.position = transform.position + new Vector3(i == 0 ? -1f : 1f, 0, 0);
            
            BonusBall splitController = splitBall.GetComponent<BonusBall>();
            if (splitController != null)
            {
                splitController.pointValue = 1;
                splitController.fallSpeed = fallSpeed * 1.2f;
                splitController.isSplitting = true; // Prevent infinite splitting
                splitController.waveAmplitude = waveAmplitude * 0.5f;
            }
        }
    }

    protected override void OnCaught()
    {
        Debug.Log($"Bonus Ball caught! Wave pattern mastered! Points: {pointValue}");
        
        // Special sound effect
        if (gameManager != null && gameManager.audioSource != null && gameManager.catchSound != null)
        {
            // Play sound twice with different pitches for bonus effect
            gameManager.audioSource.pitch = 1.2f;
            gameManager.audioSource.PlayOneShot(gameManager.catchSound);
            gameManager.audioSource.pitch = 1.4f;
            gameManager.audioSource.PlayOneShot(gameManager.catchSound);
            gameManager.audioSource.pitch = 1f;
        }
    }
}