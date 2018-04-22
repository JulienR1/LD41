using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingCart : Obstacle, IInteractable
{
    public Transform[] models;
    public int fullCart;

    private int itemPerModel;
    private int modelToUse;

    protected override void Start()
    {
        base.Start();

        modelToUse = 0;
        itemPerModel = fullCart / models.Length;
    }

    //----------------------------------------------------------------------------------------------------
    // Called when a player interacts with the object
    //----------------------------------------------------------------------------------------------------
    public bool Interact()
    {
        Main.instance.GetInventoryUI().ToggleInventory(-1);
        int savedAmount = Inventory.SaveInventory();

        modelToUse = savedAmount / itemPerModel;
        for (int i = 0; i < models.Length; i++)
            models[i].gameObject.SetActive(i == modelToUse);

        return true;
    }
}