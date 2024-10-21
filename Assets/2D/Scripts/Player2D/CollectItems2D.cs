using UnityEngine;
using UnityEngine.UI;

public class CollectItems2D : MonoBehaviour
{

    public int NumberOfSticks { get; set; }
    public int TotalSticksNeeded = 3;

    public Text SticksUI;


    void Start()
    {
        UpdateSticksUI();
    }


    public void StickCollected()
    {
        NumberOfSticks++;
        UpdateSticksUI();
    }

    public void UpdateSticksUI()
    {
        // Update the text on the UI element
        SticksUI.text = "Sticks: " + NumberOfSticks + " / " + TotalSticksNeeded;
    }
}
