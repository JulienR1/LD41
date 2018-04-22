using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour {

    public Image icon;
    public Text savedText, unsavedText;

    public void UpdateUI(Sprite _icon, string _savedText, string _unsavedText)
    {
        icon.sprite = _icon;
        savedText.text = _savedText;
        unsavedText.text = _unsavedText;
    }	
}