using UnityEngine;


public class viewAngle : MonoBehaviour
{
    public Transform target;
    // Update is called once per frame
    void Update()
    {
        // This keeps the magnified view looking the correct way.
        transform.LookAt(target);
        transform.Rotate(0f, 180f, 0f);
    }
}
