using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryUI))]
public static class Inventory {
    public enum InventoryType { MAIN, TEMP }

    public static List<Slot> currentSlots;
    public static List<Slot> savedSlots;

    //----------------------------------------------------------------------------------------------------
    // Default everything to the correct values
    //----------------------------------------------------------------------------------------------------
    public static void Initialize()
    {
        currentSlots = new List<Slot>();
        savedSlots = new List<Slot>();
    }

    //----------------------------------------------------------------------------------------------------
    // Add all the items that are contained in the loot
    //----------------------------------------------------------------------------------------------------
    public static void AddLoot(Loot loot, bool mainInventory)
    {
        foreach (Loot.ItemDrop iD in loot.items)
        {
            AddItem(iD.item, Random.Range(iD.minQuantity, iD.maxQuantity), mainInventory);
        }
    }

    //---------------------------------------------------------------------------------------------------
    // Called when an item is picked up, being new or already discovered
    //---------------------------------------------------------------------------------------------------
    public static bool AddItem(Item item, int amount, bool mainInventory)
    {
        List<Slot> currentInventory = LoadInventory(mainInventory);

        if (amount > 0)
        {
            int index = GetSlotIndex(item, ref currentInventory);
            if (index >= currentInventory.Count)
                currentInventory.Add(new Slot(item));
            currentInventory[index].AddItem(amount);

            // UI ADDING ITEMS
            return true;
        }
        CloseInventory(mainInventory, currentInventory);
        return false;
    }

    //----------------------------------------------------------------------------------------------------
    // Called when a food is purchased. Makes sure that there is enough items
    //----------------------------------------------------------------------------------------------------
    public static bool RemoveItem(Item item, int amount, bool mainInventory)
    {
        if (amount > 0)
        {
            List<Slot> currentInventory = LoadInventory(mainInventory);

            int index = GetSlotIndex(item, ref currentInventory);
            if (currentInventory[index].Amount - amount >= 0)
            {
                currentInventory[index].AddItem(-amount);
                // UI REMOVING ITEMS
                return true;
            }
            CloseInventory(mainInventory, currentInventory);
        }
        // UI NOT ENOUGH ITEMS
        return false;
    }

    //---------------------------------------------------------------------------------------------------
    // Adds the items from the temporary inventory to the saved one
    //---------------------------------------------------------------------------------------------------
    public static int SaveInventory()
    {
        for (int i = 0; i < currentSlots.Count; i++)
        {
            AddItem(currentSlots[i].GetItem(), currentSlots[i].Amount, true);           
        }
        currentSlots.Clear();
        return savedSlots.Count;
    }

    //----------------------------------------------------------------------------------------------------
    // Returns the index of the slot containing the wanted item
    //----------------------------------------------------------------------------------------------------
    private static int GetSlotIndex(Item item, ref List<Slot> inventory)
    {
        int i = 0;
        for (; i < inventory.Count; i++)
            if (inventory[i].GetItem() == item)
                break;
        return i;
    }

    //----------------------------------------------------------------------------------------------------
    // Returns the values of the correct inventory to use
    //----------------------------------------------------------------------------------------------------
    private static List<Slot> LoadInventory(bool isMainInvetory)
    {
        return (isMainInvetory) ? savedSlots : currentSlots;
    }

    //----------------------------------------------------------------------------------------------------
    // Sets the new values to the correct inventory
    //----------------------------------------------------------------------------------------------------
    private static void CloseInventory(bool isMainInventory, List<Slot> currentInventory)
    {
        if (isMainInventory)
            savedSlots = currentInventory;
        else
            currentSlots = currentInventory;
    }

    //---------------------------------------------------------------------------------------------------
    // The class used to link the items to the inventory
    //---------------------------------------------------------------------------------------------------
    public class Slot
    {
        private Item item;
        private int amount;

        public int Amount { get { return amount; } }

        public Slot(Item _item)
        {
            item = _item;
            amount = 0;
        }

        public void AddItem(int qty)
        {
            amount += qty;
        }

        public Item GetItem()
        {
            return item;
        }

        public override string ToString()
        {
            return item + ": " + amount;
        }
    }
}
