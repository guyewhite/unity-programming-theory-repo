using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PolymorphismDemo : MonoBehaviour
{
    public GameObject normalBallPrefab;
    public GameObject goldenBallPrefab;
    public GameObject speedBallPrefab;
    public GameObject bonusBallPrefab;
    public GameObject powerUpPrefab;
    
    private List<BallController> activeBalls = new List<BallController>();
    
    void Start()
    {
        Debug.Log("=== POLYMORPHISM DEMONSTRATION ===");
        Debug.Log("Press number keys 1-5 to see different demonstrations:");
        Debug.Log("1 - Method Overloading");
        Debug.Log("2 - Polymorphic Movement");
        Debug.Log("3 - Polymorphic Forces");
        Debug.Log("4 - Polymorphic Initialization");
        Debug.Log("5 - Power-Ups & Abstraction");
    }
    
    void Update()
    {
        // Using new Input System compatible check
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) 
        {
            Debug.LogWarning("Keyboard not detected! Make sure the new Input System is configured.");
            return;
        }
        
        // Press different keys to demonstrate polymorphism
        if (keyboard.digit1Key.wasPressedThisFrame)
        {
            Debug.Log("Key 1 pressed!");
            DemonstrateMethodOverloading();
        }
        
        if (keyboard.digit2Key.wasPressedThisFrame)
        {
            Debug.Log("Key 2 pressed!");
            DemonstratePolymorphicMovement();
        }
        
        if (keyboard.digit3Key.wasPressedThisFrame)
        {
            Debug.Log("Key 3 pressed!");
            DemonstratePolymorphicForces();
        }
        
        if (keyboard.digit4Key.wasPressedThisFrame)
        {
            Debug.Log("Key 4 pressed!");
            DemonstratePolymorphicInitialization();
        }
        
        if (keyboard.digit5Key.wasPressedThisFrame)
        {
            Debug.Log("Key 5 pressed!");
            DemonstratePowerUps();
        }
    }
    
    void DemonstrateMethodOverloading()
    {
        Debug.Log("\n--- METHOD OVERLOADING DEMO ---");
        
        // Create a ball and call different versions of Initialize
        if (normalBallPrefab != null)
        {
            GameObject ball = Instantiate(normalBallPrefab, Vector3.zero, Quaternion.identity);
            BallController controller = ball.GetComponent<BallController>();
            
            if (controller != null)
            {
                // Same method name, different parameters
                controller.Initialize();                           // No parameters
                controller.Initialize(10f);                        // One float
                controller.Initialize(10f, 5);                     // Float and int
                controller.Initialize(Vector3.one * 5, Color.red); // Vector3 and Color
                
                activeBalls.Add(controller);
            }
        }
    }
    
    void DemonstratePolymorphicMovement()
    {
        Debug.Log("\n--- POLYMORPHIC MOVEMENT DEMO ---");
        Debug.Log("All balls have Move() method but each moves differently:");
        
        Vector3 spawnPos = Vector3.up * 5;
        float spacing = 3f;
        
        // Spawn different ball types - all are BallController but move differently
        SpawnAndDescribe(normalBallPrefab, spawnPos + Vector3.left * spacing * 1.5f, 
            "Normal Ball: Falls straight down");
        
        SpawnAndDescribe(goldenBallPrefab, spawnPos + Vector3.left * spacing * 0.5f, 
            "Golden Ball: Zigzag movement");
        
        SpawnAndDescribe(speedBallPrefab, spawnPos + Vector3.right * spacing * 0.5f, 
            "Speed Ball: Spiral movement with acceleration");
        
        SpawnAndDescribe(bonusBallPrefab, spawnPos + Vector3.right * spacing * 1.5f, 
            "Bonus Ball: Wave pattern movement");
    }
    
    void DemonstratePolymorphicForces()
    {
        Debug.Log("\n--- POLYMORPHIC FORCE APPLICATION DEMO ---");
        Debug.Log("Same ApplyForce() method, different responses:");
        
        // Apply the same force to all active balls - each responds differently
        foreach (BallController ball in activeBalls)
        {
            if (ball != null)
            {
                // Method overloading - different ways to apply force
                ball.ApplyForce(5f);                    // Single float
                ball.ApplyForce(Vector3.right * 3f);    // Vector3
                ball.ApplyForce(2f, Vector3.up);        // Float and direction
            }
        }
    }
    
    void DemonstratePolymorphicInitialization()
    {
        Debug.Log("\n--- POLYMORPHIC INITIALIZATION DEMO ---");
        Debug.Log("Same Initialize() call, different behaviors per ball type:");
        
        // Create an array of different ball types, all treated as BallController
        BallController[] balls = new BallController[4];
        
        Vector3 startPos = Vector3.up * 3;
        
        if (normalBallPrefab != null)
            balls[0] = CreateBall(normalBallPrefab, startPos + Vector3.left * 4);
        
        if (goldenBallPrefab != null)
            balls[1] = CreateBall(goldenBallPrefab, startPos + Vector3.left * 2);
        
        if (speedBallPrefab != null)
            balls[2] = CreateBall(speedBallPrefab, startPos + Vector3.right * 2);
        
        if (bonusBallPrefab != null)
            balls[3] = CreateBall(bonusBallPrefab, startPos + Vector3.right * 4);
        
        // Call the same method on all balls - each responds based on its type
        foreach (BallController ball in balls)
        {
            if (ball != null)
            {
                ball.Initialize(7f);  // Same call, different behavior per ball type
            }
        }
    }
    
    BallController CreateBall(GameObject prefab, Vector3 position)
    {
        if (prefab != null)
        {
            GameObject ball = Instantiate(prefab, position, Quaternion.identity);
            BallController controller = ball.GetComponent<BallController>();
            if (controller != null)
            {
                activeBalls.Add(controller);
                return controller;
            }
        }
        return null;
    }
    
    void SpawnAndDescribe(GameObject prefab, Vector3 position, string description)
    {
        if (prefab != null)
        {
            GameObject ball = Instantiate(prefab, position, Quaternion.identity);
            BallController controller = ball.GetComponent<BallController>();
            if (controller != null)
            {
                activeBalls.Add(controller);
                Debug.Log(description);
            }
        }
    }
    
    void DemonstratePowerUps()
    {
        Debug.Log("\n--- POWER-UP & ABSTRACTION DEMO ---");
        Debug.Log("Spawning a power-up that implements ICollectable interface!");
        Debug.Log("This demonstrates ABSTRACTION - it defines WHAT to do, not HOW");
        
        if (powerUpPrefab != null)
        {
            // Spawn one power-up at random position
            float randomX = Random.Range(-3f, 3f);
            Vector3 spawnPosition = new Vector3(randomX, 6f, 0);
            
            GameObject powerUp = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
            
            PowerUp powerUpScript = powerUp.GetComponent<PowerUp>();
            if (powerUpScript != null)
            {
                // Set a random power-up type
                powerUpScript.powerType = (PowerUp.PowerUpType)Random.Range(0, 3);
                
                // Make it visible
                powerUp.transform.localScale = Vector3.one * 0.75f;
                
                // Demonstrate interface abstraction
                ICollectable collectable = powerUpScript as ICollectable;
                if (collectable != null)
                {
                    Debug.Log($"Power-Up Type: {collectable.GetCollectableType()}");
                    Debug.Log($"Point Value: {collectable.GetPointValue()}");
                    Debug.Log($"Can be collected: {collectable.CanBeCollected()}");
                }
            }
            
            // Make it fall at normal speed like balls
            Rigidbody rb = powerUp.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = powerUp.AddComponent<Rigidbody>();
            }
            rb.useGravity = false; // We'll control the fall manually
            
            // Add a simple falling script component
            powerUp.AddComponent<SimpleFall>();
            
            Debug.Log("Power-ups spawned! They implement the abstract ICollectable interface.");
            Debug.Log("Each power-up defines its own implementation of the interface methods.");
        }
        else
        {
            Debug.LogWarning("Power-up prefab not assigned! Please assign it in the Inspector.");
        }
    }
    
    void OnDestroy()
    {
        // Clean up
        foreach (BallController ball in activeBalls)
        {
            if (ball != null)
            {
                Destroy(ball.gameObject);
            }
        }
        activeBalls.Clear();
    }
}