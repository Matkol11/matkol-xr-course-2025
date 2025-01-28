using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{  
    public Transform location1;
    public Transform location2;
    private bool isAtSpawn = true;
    public InputActionReference action;
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            SwitchPlane();
        };
    }

    private void SwitchPlane()
    {
        if(isAtSpawn)
        {
            transform.position = location2.position;
            isAtSpawn = false;
        }else{
            transform.position = location1.position;
            isAtSpawn = true;
        }
        
    }

}
