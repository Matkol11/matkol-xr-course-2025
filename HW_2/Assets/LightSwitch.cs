using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public Light light;
    public Color[] colors;
    private int currentColorIndex = 0; 
    public InputActionReference action;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = GetComponent<Light>();

        light.color = colors[currentColorIndex];

        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            CycleColor();
        };
    }

    private void CycleColor()
    {
        if (colors.Length == 0) return;

        currentColorIndex = (currentColorIndex + 1) % colors.Length;

        light.color = colors[currentColorIndex];
    }
}    
