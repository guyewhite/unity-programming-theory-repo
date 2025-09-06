using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject goldenBallPrefab;
    public GameObject speedBallPrefab;
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

    void SpawnBall()
    {
        GameObject ballToSpawn = ChooseBallType();
        
        if (ballToSpawn != null)
        {
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0f);
            
            if (spawnArea != null)
            {
                spawnPosition = spawnArea.position + new Vector3(randomX, 0f, 0f);
            }
            
            Instantiate(ballToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    GameObject ChooseBallType()
    {
        float randomValue = Random.Range(0f, 100f);
        
        if (randomValue < 10f && goldenBallPrefab != null)
        {
            Debug.Log("Spawning Golden Ball!");
            return goldenBallPrefab;
        }
        else if (randomValue < 30f && speedBallPrefab != null)
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