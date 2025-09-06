using UnityEngine;

public class SpeedBall : BallController
{
    private float speedIncreaseRate = 2f;
    private ParticleSystem speedTrail;

    protected override void Start()
    {
        fallSpeed = 8f;
        pointValue = 3;
        ballColor = Color.red;
        
        base.Start();
        CreateSpeedTrail();
    }

    protected override void SetupBall()
    {
        base.SetupBall();
        
        transform.localScale = Vector3.one * 0.8f;
        
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = new Color(1f, 0.2f, 0.2f);
        }
    }

    private void CreateSpeedTrail()
    {
        GameObject trailObj = new GameObject("SpeedTrail");
        trailObj.transform.SetParent(transform);
        trailObj.transform.localPosition = Vector3.zero;
        
        speedTrail = trailObj.AddComponent<ParticleSystem>();
        var main = speedTrail.main;
        main.startLifetime = 0.5f;
        main.startSpeed = 0.1f;
        main.startSize = 0.3f;
        main.startColor = new Color(1f, 0.5f, 0f, 0.5f);
        
        var emission = speedTrail.emission;
        emission.rateOverTime = 30f;
        
        var shape = speedTrail.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 0.1f;
        
        var velocityOverLifetime = speedTrail.velocityOverLifetime;
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(2f);
        
        var renderer = speedTrail.GetComponent<ParticleSystemRenderer>();
        renderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    protected override void Update()
    {
        fallSpeed += speedIncreaseRate * Time.deltaTime;
        
        base.Update();
        
        transform.Rotate(Vector3.forward * 200f * Time.deltaTime);
    }

    protected override void OnCaught()
    {
        Debug.Log("Speed Ball caught! Quick reflexes!");
        
        if (gameManager != null && gameManager.audioSource != null && gameManager.catchSound != null)
        {
            gameManager.audioSource.pitch = 2f;
            gameManager.audioSource.PlayOneShot(gameManager.catchSound);
            gameManager.audioSource.pitch = 1f;
        }
    }
}