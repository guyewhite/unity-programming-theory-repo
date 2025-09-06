using UnityEngine;

// ABSTRACTION EXAMPLE: Interface
// This interface defines what ALL collectable items MUST do,
// but not HOW they do it. Each implementation decides the "how".
public interface ICollectable
{
    // Abstract properties - WHAT data collectables must have
    int GetPointValue();
    string GetCollectableType();
    
    // Abstract methods - WHAT actions collectables must perform
    void OnCollected(GameObject collector);
    void PlayCollectionEffect();
    bool CanBeCollected();
}

// ABSTRACTION EXAMPLE: Abstract Class
// This abstract class provides a template with both abstract and concrete members
// It defines the general structure but leaves specific details to child classes
public abstract class CollectableItem : MonoBehaviour
{
    // Concrete fields that all collectables share
    protected int basePointValue = 1;
    protected AudioClip collectSound;
    protected ParticleSystem collectEffect;
    
    // Abstract methods - MUST be implemented by child classes
    // These define WHAT must happen but not HOW
    public abstract void PerformCollection();
    public abstract float GetCollectionDifficulty();
    public abstract Color GetVisualColor();
    
    // Virtual method - CAN be overridden but has default behavior
    // This is partial abstraction - provides default but allows customization
    public virtual void Initialize()
    {
        Debug.Log($"Initializing collectable: {GetType().Name}");
        ApplyVisualColor();
    }
    
    // Concrete method using abstract methods
    // This shows how abstraction allows us to write code that works
    // with concepts without knowing specific implementations
    protected void ApplyVisualColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Uses abstract method - we don't know what color,
            // but we know every collectable WILL have one
            renderer.material.color = GetVisualColor();
        }
    }
    
    // Template method pattern - defines algorithm structure
    // but delegates specific steps to child classes
    public void Collect(GameObject collector)
    {
        if (!CanCollect()) return;
        
        // These are abstract - child classes define the specifics
        PerformCollection();
        float difficulty = GetCollectionDifficulty();
        
        // Reward based on difficulty (concrete logic using abstract data)
        int finalPoints = Mathf.RoundToInt(basePointValue * difficulty);
        AwardPoints(finalPoints);
        
        Destroy(gameObject);
    }
    
    // Another abstract method
    protected abstract bool CanCollect();
    
    // Concrete helper method
    private void AwardPoints(int points)
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.BallCaught(points);
        }
    }
}