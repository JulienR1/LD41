using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    public static Main instance;
    private Player player;
    private InventoryUI inventoryUI;

    public Item[] items;
    public Transform counterSpawn;

    private void Awake()
    {
        instance = this;

        player = FindObjectOfType<Player>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        FindObjectOfType<MapGenerator>().Initialize();
        Inventory.Initialize();

        foreach (Item i in items)
            Inventory.AddItem(i, 3, false);
    }

    //---------------------------------------------------------------------------------------------------
    // Controls the inventory ui
    //--------------------------------------------------------------------------------------------------
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUI.ToggleInventory();
        }
    }

    //----------------------------------------------------------------------------------------------------
    // Sets the player to the right position depending on the room
    //----------------------------------------------------------------------------------------------------
    public void SetPlayerPosition(bool inBackstore)
    {
        if (inBackstore)
        {
            player.transform.position = Map.SpawnPoint.Position;
        }
        else
        {
            player.transform.position = counterSpawn.position;
        }
    }

    public InventoryUI GetInventoryUI()
    {
        if (inventoryUI != null)
        {
            return inventoryUI;
        }
        return null;
    }
}
