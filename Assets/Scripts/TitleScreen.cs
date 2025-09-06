using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    public GameObject titleScreenPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI instructionText;
    public GameManager gameManager;
    public AudioSource backgroundMusicSource;
    public AudioClip ambientMusic;
    
    private bool gameStarted = false;
    
    void Start()
    {
        SetupBackgroundMusic();
        ShowTitleScreen();
        
        if (titleText != null)
        {
            titleText.text = "CATCH THE BALL";
            titleText.fontSize = 72;
            titleText.color = Color.white;
        }
        
        if (instructionText != null)
        {
            instructionText.text = "Press SPACE BAR to play game";
            instructionText.fontSize = 36;
            instructionText.color = new Color(0.8f, 0.8f, 0.8f);
        }
    }
    
    void SetupBackgroundMusic()
    {
        if (backgroundMusicSource == null)
        {
            GameObject musicObject = GameObject.Find("BackgroundMusic");
            if (musicObject == null)
            {
                musicObject = new GameObject("BackgroundMusic");
                DontDestroyOnLoad(musicObject);
            }
            backgroundMusicSource = musicObject.GetComponent<AudioSource>();
            if (backgroundMusicSource == null)
            {
                backgroundMusicSource = musicObject.AddComponent<AudioSource>();
            }
        }
        
        if (ambientMusic != null && backgroundMusicSource != null)
        {
            backgroundMusicSource.clip = ambientMusic;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.volume = 0.5f;
            backgroundMusicSource.playOnAwake = false;
            
            if (!backgroundMusicSource.isPlaying)
            {
                backgroundMusicSource.Play();
            }
        }
    }
    
    void Update()
    {
        if (!gameStarted)
        {
            Keyboard keyboard = Keyboard.current;
            if (keyboard != null && keyboard.spaceKey.wasPressedThisFrame)
            {
                StartGame();
            }
        }
    }
    
    void ShowTitleScreen()
    {
        if (titleScreenPanel != null)
        {
            titleScreenPanel.SetActive(true);
        }
        
        Time.timeScale = 0f;
        
        if (gameManager != null)
        {
            gameManager.enabled = false;
        }
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = false;
            }
        }
    }
    
    void StartGame()
    {
        gameStarted = true;
        
        if (titleScreenPanel != null)
        {
            titleScreenPanel.SetActive(false);
        }
        
        Time.timeScale = 1f;
        
        if (gameManager != null)
        {
            gameManager.enabled = true;
        }
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }
        
        Debug.Log("Game Started!");
    }
}