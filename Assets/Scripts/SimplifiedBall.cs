using UnityEngine;

// CONCRETE IMPLEMENTATION of CollectableItem abstract class
// This demonstrates ABSTRACTION by extending the abstract template
public class SimplifiedBall : CollectableItem
{
    private float fallSpeed = 5f;
    private Color ballColor = Color.blue;
    private float difficulty = 1f;
    
    void Start()
    {
        basePointValue = 3;
        Initialize(); // Calls the virtual method from abstract parent
    }
    
    void Update()
    {
        // Simple falling behavior
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
    
    // REQUIRED: Must implement all abstract methods from CollectableItem
    
    // Implementation of abstract method - defines HOW this specific ball performs collection
    public override void PerformCollection()
    {
        Debug.Log("SimplifiedBall: Performing collection animation");
        transform.localScale = Vector3.one * 2f;
        GetComponent<Renderer>().material.color = Color.white;
    }
    
    // Implementation of abstract method - defines this ball's difficulty
    public override float GetCollectionDifficulty()
    {
        // Difficulty based on fall speed
        difficulty = fallSpeed / 5f;
        return difficulty;
    }
    
    // Implementation of abstract method - defines this ball's color
    public override Color GetVisualColor()
    {
        return ballColor;
    }
    
    // Implementation of abstract method - defines when this can be collected
    protected override bool CanCollect()
    {
        // Can only collect if ball is above ground level
        return transform.position.y > -5f;
    }
    
    // Override the virtual Initialize method to add specific behavior
    public override void Initialize()
    {
        base.Initialize(); // Call parent's initialization
        
        // Add specific initialization for SimplifiedBall
        fallSpeed = Random.Range(3f, 8f);
        ballColor = new Color(
            Random.Range(0.5f, 1f),
            Random.Range(0.5f, 1f),
            Random.Range(0.5f, 1f)
        );
        
        ApplyVisualColor(); // Use inherited concrete method
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Use the template method from abstract parent
            Collect(other.gameObject);
        }
    }
}