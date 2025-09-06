using UnityEngine;

// CONCRETE IMPLEMENTATION of the ICollectable interface
// This class MUST implement all methods defined in ICollectable
public class PowerUp : MonoBehaviour, ICollectable
{
    public enum PowerUpType { DoublePoints, SlowTime, Shield }
    public PowerUpType powerType = PowerUpType.DoublePoints;
    
    private float rotationSpeed = 100f;
    private float bobSpeed = 2f;
    private float bobHeight = 0.5f;
    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        // Visual effects
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        
        // Only bob if we have a SimpleFall component (for demo), otherwise fall and bob
        SimpleFall fallComponent = GetComponent<SimpleFall>();
        if (fallComponent == null)
        {
            // Normal bobbing behavior when spawned in regular game
            float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        else
        {
            // When spawned by demo, update start position as we fall
            startPosition = transform.position;
        }
    }
    
    // IMPLEMENTATION of abstract interface methods
    // We MUST provide these because ICollectable requires them
    
    public int GetPointValue()
    {
        // Different power-ups give different points
        switch (powerType)
        {
            case PowerUpType.DoublePoints: return 10;
            case PowerUpType.SlowTime: return 5;
            case PowerUpType.Shield: return 8;
            default: return 5;
        }
    }
    
    public string GetCollectableType()
    {
        return $"PowerUp_{powerType}";
    }
    
    public void OnCollected(GameObject collector)
    {
        Debug.Log($"Power-up collected: {powerType}");
        ApplyPowerUpEffect(collector);
        PlayCollectionEffect();
        Destroy(gameObject);
    }
    
    public void PlayCollectionEffect()
    {
        // Create a visual effect when collected
        GameObject effect = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        effect.transform.position = transform.position;
        effect.transform.localScale = Vector3.one * 3f;
        
        Renderer renderer = effect.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = GetPowerUpColor();
        }
        
        Destroy(effect, 0.5f);
    }
    
    public bool CanBeCollected()
    {
        // Power-ups can always be collected
        return true;
    }
    
    // Additional concrete methods specific to PowerUp
    private void ApplyPowerUpEffect(GameObject collector)
    {
        switch (powerType)
        {
            case PowerUpType.DoublePoints:
                Debug.Log("Double points activated for 10 seconds!");
                break;
            case PowerUpType.SlowTime:
                Time.timeScale = 0.5f;
                Invoke("ResetTimeScale", 5f);
                break;
            case PowerUpType.Shield:
                Debug.Log("Shield activated! Immune to missing balls!");
                break;
        }
    }
    
    private void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }
    
    private Color GetPowerUpColor()
    {
        switch (powerType)
        {
            case PowerUpType.DoublePoints: return Color.yellow;
            case PowerUpType.SlowTime: return Color.cyan;
            case PowerUpType.Shield: return Color.magenta;
            default: return Color.white;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected(other.gameObject);
        }
    }
}