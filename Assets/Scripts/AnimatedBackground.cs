using UnityEngine;

public class AnimatedBackground : MonoBehaviour
{
    public Gradient colorGradient;
    public float colorChangeSpeed = 1f;
    public bool animateColors = true;
    
    private Camera mainCamera;
    private float time = 0;

    void Awake()
    {
        SetupDefaultGradient();
    }
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    void SetupDefaultGradient()
    {
        colorGradient = new Gradient();
        
        GradientColorKey[] colorKeys = new GradientColorKey[5];
        colorKeys[0] = new GradientColorKey(new Color(0.2f, 0.1f, 0.5f), 0.0f);
        colorKeys[1] = new GradientColorKey(new Color(0.5f, 0.2f, 0.8f), 0.25f);
        colorKeys[2] = new GradientColorKey(new Color(0.9f, 0.4f, 0.6f), 0.5f);
        colorKeys[3] = new GradientColorKey(new Color(1.0f, 0.6f, 0.3f), 0.75f);
        colorKeys[4] = new GradientColorKey(new Color(0.2f, 0.1f, 0.5f), 1.0f);
        
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphaKeys[1] = new GradientAlphaKey(1.0f, 1.0f);
        
        colorGradient.SetKeys(colorKeys, alphaKeys);
    }

    void Update()
    {
        if (mainCamera != null && animateColors)
        {
            time += Time.deltaTime * colorChangeSpeed;
            float normalizedTime = Mathf.PingPong(time, 1f);
            mainCamera.backgroundColor = colorGradient.Evaluate(normalizedTime);
        }
    }
}