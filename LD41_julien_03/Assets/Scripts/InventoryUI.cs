using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    private enum InventoryState { OPEN, CLOSE }
    private InventoryState inventoryState;

    public Sprite[] bagSprites;

    public SlotUI slotPrefab;
    public RectTransform bagPosition;
    public RectTransform slotsHolder;
    public float animationTime = 0.5f;
    public float slotOffset;

    private bool isInventoryOpen;

    private List<Transform> allInventorySlots;

    private void Start()
    {
        inventoryState = InventoryState.CLOSE;
        allInventorySlots = new List<Transform>();
    }

    //----------------------------------------------------------------------------------------------------
    // Opens and closes the inventory. Generates the slots according to the items collected
    //----------------------------------------------------------------------------------------------------
    public void ToggleInventory(int state = 0)
    {
        if (state == -1)
            inventoryState = InventoryState.CLOSE;
        else if (state == 1)
            inventoryState = InventoryState.OPEN;
        else
            inventoryState = (InventoryState)(1 - inventoryState);

        if (inventoryState == InventoryState.OPEN)
        {
            for (int i = 0; i < Inventory.currentSlots.Count; i++)
            {
                RectTransform t = Instantiate(slotPrefab.GetComponent<RectTransform>(), slotsHolder);

                int savedCount = GetSavedCountt(Inventory.currentSlots[i].GetItem());
                Inventory.Slot currentSlot = Inventory.currentSlots[i];
                t.GetComponent<SlotUI>().UpdateUI(currentSlot.GetItem().sprite, savedCount + "x saved", currentSlot.Amount + "x in hands");

                t.gameObject.SetActive(false);
                t.localPosition = bagPosition.localPosition;
                allInventorySlots.Add(t);
            }
            StartCoroutine("OpenAnimation");
        }
        else
        {
            for (int i = 0; i < slotsHolder.childCount; i++)
                GameObject.Destroy(slotsHolder.GetChild(i).gameObject);
        }
    }

    //----------------------------------------------------------------------------------------------------
    // Get how many of the particular item is saved
    //----------------------------------------------------------------------------------------------------
    public int GetSavedCountt(Item item)
    {
        for(int i = 0; i < Inventory.savedSlots.Count; i++)
        {
            if (item.Equals(Inventory.savedSlots[i].GetItem()))
            {
                return Inventory.savedSlots[i].Amount;
            }
        }
        return 0;
    }

    //------------------------------------------------------------------------------------------------
    // Displays the slots sliding from or toward the bag
    //------------------------------------------------------------------------------------------------
    IEnumerator OpenAnimation()
    {
        if (allInventorySlots.Count > 0)
        {
            Vector3[] endPos = new Vector3[allInventorySlots.Count];
            for (int i = 0; i < allInventorySlots.Count; i++)
            {
                allInventorySlots[i].gameObject.SetActive(true);
                endPos[i] = bagPosition.localPosition - (i + 1.5f) * slotOffset * Vector3.up;
            }

            float elapsedTime = 0;
            float percent;
            while (elapsedTime < animationTime)
            {
                percent = elapsedTime / animationTime;
                for (int i = 0; i < allInventorySlots.Count; i++)
                {
                    allInventorySlots[i].localPosition = Vector3.Lerp(bagPosition.localPosition, endPos[i], percent);
                }
                yield return null;
                elapsedTime += Time.deltaTime;
            }
        }
    }
}