using UnityEngine;

public class ParticleEffects : MonoBehaviour
{
    private ParticleSystem starParticles;
    
    void Start()
    {
        CreateStarfield();
    }

    void CreateStarfield()
    {
        GameObject starfieldObj = new GameObject("Starfield");
        starfieldObj.transform.position = new Vector3(0, 5, 5);
        
        starParticles = starfieldObj.AddComponent<ParticleSystem>();
        var main = starParticles.main;
        main.startLifetime = 10f;
        main.startSpeed = 2f;
        main.startSize = 0.1f;
        main.startColor = Color.white;
        main.maxParticles = 100;
        
        var emission = starParticles.emission;
        emission.rateOverTime = 10f;
        
        var shape = starParticles.shape;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = new Vector3(20, 1, 1);
        
        var velocityOverLifetime = starParticles.velocityOverLifetime;
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(-3f);
        
        var renderer = starParticles.GetComponent<ParticleSystemRenderer>();
        renderer.material = new Material(Shader.Find("Sprites/Default"));
    }
}