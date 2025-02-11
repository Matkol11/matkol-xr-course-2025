using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    // This script should be attached to both controller objects in the scene
    // Make sure to define the input in the editor (LeftHand/Grip and RightHand/Grip recommended respectively)
    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    bool grabbing = false;

    private Vector3 previousPos;
    private Quaternion previousRot;

    private void Start()
    {
        action.action.Enable();

        // Find the other hand
        foreach(CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
    }

    void Update()
    {
        grabbing = action.action.IsPressed();
        if (grabbing)
        {
            // Grab nearby object or the object in the other hand
            if (!grabbedObject)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

            if (grabbedObject)
            {
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.isKinematic = true;

                Vector3 controllerToObject = grabbedObject.position - transform.position;
                Vector3 deltaPos = transform.position - previousPos;
                Quaternion deltaRot = transform.rotation * Quaternion.Inverse(previousRot);

                if (otherHand.grabbedObject == grabbedObject)
                {
                    Vector3 sumDeltaPos = (deltaPos + otherHand.GetDeltaPos()) * 0.5f;
                    Quaternion sumDeltaRot = deltaRot * otherHand.GetDeltaRot();

                    grabbedObject.position = sumDeltaPos + grabbedObject.position;
                    grabbedObject.position = grabbedObject.position + sumDeltaRot * controllerToObject - controllerToObject;
                    grabbedObject.rotation = sumDeltaRot * grabbedObject.rotation;
                } else {
                    grabbedObject.position = deltaPos + grabbedObject.position;
                    grabbedObject.position = grabbedObject.position + deltaRot * controllerToObject - controllerToObject;
                    grabbedObject.rotation = deltaRot * grabbedObject.rotation;
                }
            }
        }
        // If let go of button, release object
        else if (grabbedObject)
        {
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            rb.useGravity = true; 
            rb.isKinematic = false;

            grabbedObject = null;
        }

        previousPos = transform.position;
        previousRot = transform.rotation;
    }

    public Vector3 GetDeltaPos()
    {
        return transform.position - previousPos;
    }

    public Quaternion GetDeltaRot()
    {
        return transform.rotation * Quaternion.Inverse(previousRot);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure to tag grabbable objects with the "grabbable" tag
        // You also need to make sure to have colliders for the grabbable objects and the controllers
        // Make sure to set the controller colliders as triggers or they will get misplaced
        // You also need to add Rigidbody to the controllers for these functions to be triggered
        // Make sure gravity is disabled though, or your controllers will (virtually) fall to the ground

        Transform t = other.transform;
        if(t && t.tag.ToLower()=="grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if( t && t.tag.ToLower()=="grabbable")
            nearObjects.Remove(t);
    }
}