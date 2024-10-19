using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItems2D : MonoBehaviour
{

    public int NumberOfSticks { get; private set; }
    public int TotalSticksNeeded = 3;

    public Text SticksUI;


    void Start()
    {
        GameObject foundObject = GameObject.Find("ItemsCollected");

        if (foundObject != null)
        {
            SticksUI = foundObject.GetComponent<Text>();
        }
        else
        {
            Debug.LogError("No object named 'ItemsCollected' found in the scene.");
        }

        UpdateSticksUI();
    }


    public void StickCollected()
    {
        NumberOfSticks++;
        UpdateSticksUI();
    }

    private void UpdateSticksUI()
    {
        // Update the text on the UI element
        SticksUI.text = "Sticks: " + NumberOfSticks + " / " + TotalSticksNeeded;
    }
}
