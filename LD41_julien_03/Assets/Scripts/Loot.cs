using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot", menuName = "Loot")]
public class Loot : ScriptableObject
{
    public new string name;
    public ItemDrop[] items;

    [System.Serializable]
    public struct ItemDrop
    {
        public Item item;
        public int minQuantity;
        public int maxQuantity;

        public int DropQuantity()
        {
            return Random.Range(minQuantity, maxQuantity + 1);
        }
    }
}