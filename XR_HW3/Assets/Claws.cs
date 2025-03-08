using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Claws : MonoBehaviour
{
    [SerializeField] private InputActionProperty gripAction;
    [SerializeField] private float gripThreshold = 0.1f;
    private Animator anim;
    public bool IsGripping { get; private set; }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float ourGripValue = gripAction.action.ReadValue<float>();
        anim.SetFloat("Grip", ourGripValue);

        IsGripping = (ourGripValue > gripThreshold);
    }
}