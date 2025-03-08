using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Counter : MonoBehaviour
{
    [Tooltip("The tag you want to count in the scene.")]
    public string targetTag = "Drop";

    [Tooltip("Current count of tagged objects in the scene.")]
    public int currentCount;
    public TMP_Text countText;
    private HashSet<GameObject> collidedObjects = new HashSet<GameObject>();

    void Start()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(targetTag);

        currentCount = taggedObjects.Length;

        UpdateCountUI();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            if (!collidedObjects.Contains(collision.gameObject))
            {
                collidedObjects.Add(collision.gameObject);

                currentCount--;
                UpdateCountUI();
            }
        }
    }
    private void UpdateCountUI()
    {
        if (countText != null)
        {
            countText.text = "Objects Not Yet On The Floor: " + currentCount;
        }
        if (countText != null)
        {
            if (currentCount <= 0)
            {
                countText.text = "Congratulations! Everything is on the floor!";
            }
        }
    }
}

