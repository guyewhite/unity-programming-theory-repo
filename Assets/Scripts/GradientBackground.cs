using UnityEngine;

public class GradientBackground : MonoBehaviour
{
    public Color topColor = new Color(0.5f, 0.7f, 1.0f);
    public Color bottomColor = new Color(1.0f, 0.5f, 0.3f);
    public float gradientHeight = 10f;
    
    private Camera cam;
    private GameObject backgroundPlane;

    void Start()
    {
        cam = Camera.main;
        CreateGradientBackground();
    }

    void CreateGradientBackground()
    {
        backgroundPlane = GameObject.CreatePrimitive(PrimitiveType.Quad);
        backgroundPlane.name = "GradientBackground";
        
        Destroy(backgroundPlane.GetComponent<Collider>());
        
        backgroundPlane.transform.position = new Vector3(0, 0, 15f);
        backgroundPlane.transform.localScale = new Vector3(30f, 20f, 1f);
        
        Material mat = new Material(Shader.Find("Unlit/GradientShader"));
        if (mat.shader == null || mat.shader.name == "Hidden/InternalErrorShader")
        {
            mat = new Material(Shader.Find("Sprites/Default"));
            mat.color = topColor;
        }
        
        backgroundPlane.GetComponent<Renderer>().material = mat;
    }
}