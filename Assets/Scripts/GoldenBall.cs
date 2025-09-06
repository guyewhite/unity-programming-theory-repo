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

    protected override void Update()
    {
        base.Update();
        
        transform.Rotate(Vector3.up * 100f * Time.deltaTime);
    }
}