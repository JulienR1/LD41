using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName ="Item")]
public class Item : ScriptableObject {

    public new string name = "New item";
    public int value = 0;
    public Transform prefab = null;
    public Sprite sprite = null;

    public override bool Equals(object other)
    {
        Item otherItem = (Item)other;
        return name.Equals(otherItem.name);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
