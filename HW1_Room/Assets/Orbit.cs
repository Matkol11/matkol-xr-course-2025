using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 20, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
