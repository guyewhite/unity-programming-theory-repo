using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject goldenBallPrefab;
    public GameObject speedBallPrefab;
    public GameObject bonusBallPrefab;
    public GameObject powerUpPrefab;
    public Transform spawnArea;
    public AudioSource audioSource;
    public AudioClip catchSound;
    public TextMeshProUGUI scoreText;
    
    private int score = 0;
    private float spawnInterval = 2f;
    private float nextSpawnTime = 0f;
    private float spawnRangeX = 7f;
    private float spawnHeight = 8f;

    void Start()
    {
        if (scoreText != null)
        {
            UpdateScoreText();
        }
        
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnBall();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    // Method Overloading Example: Different ways to spawn balls
    
    // Spawn with default random position
    void SpawnBall()
    {
        GameObject ballToSpawn = ChooseBallType();
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        SpawnBall(ballToSpawn, randomX);
    }
    
    // Spawn specific ball type at specific X position
    void SpawnBall(GameObject ballPrefab, float xPosition)
    {
        if (ballPrefab != null)
        {
            Vector3 spawnPosition = new Vector3(xPosition, spawnHeight, 0f);
            
            if (spawnArea != null)
            {
                spawnPosition = spawnArea.position + new Vector3(xPosition, 0f, 0f);
            }
            
            GameObject ball = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
            Debug.Log($"Spawned {ballPrefab.name} at X: {xPosition}");
        }
    }
    
    // Spawn with custom position and initial velocity
    void SpawnBall(Vector3 position, Vector3 initialVelocity)
    {
        GameObject ballToSpawn = ChooseBallType();
        if (ballToSpawn != null)
        {
            GameObject ball = Instantiate(ballToSpawn, position, Quaternion.identity);
            BallController controller = ball.GetComponent<BallController>();
            if (controller != null)
            {
                controller.Initialize(initialVelocity, Color.white);
            }
        }
    }
    
    // Spawn with specific parameters
    void SpawnBall(GameObject ballPrefab, Vector3 position, float speed, int points)
    {
        if (ballPrefab != null)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            BallController controller = ball.GetComponent<BallController>();
            if (controller != null)
            {
                controller.Initialize(speed, points);
            }
        }
    }

    GameObject ChooseBallType()
    {
        float randomValue = Random.Range(0f, 100f);
        
        // ABSTRACTION IN ACTION: We can spawn power-ups using the same system
        // because they implement the ICollectable interface
        if (randomValue < 5f && powerUpPrefab != null)
        {
            Debug.Log("Spawning Power-Up!");
            return powerUpPrefab;
        }
        else if (randomValue < 15f && goldenBallPrefab != null)
        {
            Debug.Log("Spawning Golden Ball!");
            return goldenBallPrefab;
        }
        else if (randomValue < 25f && bonusBallPrefab != null)
        {
            Debug.Log("Spawning Bonus Ball!");
            return bonusBallPrefab;
        }
        else if (randomValue < 40f && speedBallPrefab != null)
        {
            Debug.Log("Spawning Speed Ball!");
            return speedBallPrefab;
        }
        else if (ballPrefab != null)
        {
            return ballPrefab;
        }
        
        return ballPrefab;
    }

    public void BallCaught(int points = 1)
    {
        score += points;
        UpdateScoreText();
        PlayCatchSound();
        
        if (spawnInterval > 0.5f)
        {
            spawnInterval -= 0.05f;
        }
    }

    public void BallMissed()
    {
        Debug.Log("Ball missed!");
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void PlayCatchSound()
    {
        if (audioSource != null && catchSound != null)
        {
            audioSource.PlayOneShot(catchSound);
        }
    }
}