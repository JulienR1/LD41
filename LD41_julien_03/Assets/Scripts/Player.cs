using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : Entity{

    private PlayerController controller;

    [Header("Player stats")]
    public float interactionRange = 2f;
    public LayerMask interactableMask;

    public Transform graphics;

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        controller.Move(currentMoveSpeed);
        controller.LookAtCursor(graphics);

        if (Input.GetKeyDown(KeyCode.E))
            Interact();          
    }

    //---------------------------------------------------------------------------------------------------
    // Finds all near interactables and interacts with the closest one
    //---------------------------------------------------------------------------------------------------
    private void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange, interactableMask);
        if (colliders.Length > 0)
        {
            foreach (Collider c in colliders)
            {
                if (c.GetComponentInParent<IInteractable>() != null)
                {
                    c.GetComponentInParent<IInteractable>().Interact();
                }
            }
        }
    }
}