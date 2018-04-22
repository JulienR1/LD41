using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Image fullBar;
    public float timeUp;
    private float endTime;

    private void Start()
    {
        UpdateValue(1);
        SetVisibility(false);
    }

    //----------------------------------------------------------------------------------------------------
    // Hides the ui after the elapsed time
    //----------------------------------------------------------------------------------------------------
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if (Time.time >= endTime)
            {
                SetVisibility(false);
            }
        }
    }

    //----------------------------------------------------------------------------------------------------
    // Set the ui to the correct value
    //----------------------------------------------------------------------------------------------------
    public void UpdateValue(float percent)
    {
        fullBar.fillAmount = percent;
    }

    //----------------------------------------------------------------------------------------------------
    // Activates and deactivates the hp bar
    //----------------------------------------------------------------------------------------------------
    public void SetVisibility(bool b)
    {
        gameObject.SetActive(b);
        if (b)
            endTime = Time.time + timeUp;
    }
}