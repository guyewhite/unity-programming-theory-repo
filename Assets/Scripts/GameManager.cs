using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
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
        if (ballPrefab != null)
        {
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0f);
            
            if (spawnArea != null)
            {
                spawnPosition = spawnArea.position + new Vector3(randomX, 0f, 0f);
            }
            
            Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void BallCaught()
    {
        score++;
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