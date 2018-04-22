using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingCart : Obstacle, IInteractable
{
    protected override void Start()
    {
        base.Start();
    }

    //----------------------------------------------------------------------------------------------------
    // Called when a player interacts with the object
    //----------------------------------------------------------------------------------------------------
    public bool Interact()
    {
        return true;
    }
}