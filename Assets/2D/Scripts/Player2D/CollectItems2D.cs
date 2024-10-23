using UnityEngine;
using UnityEngine.UI;
using ScriptableObjectArchitecture;

public class CollectItems2D : MonoBehaviour
{
    [SerializeField] GameEvent _onAllSticksCollectedEvent = default(GameEvent);

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

        if (NumberOfSticks >= TotalSticksNeeded)
        {
            _onAllSticksCollectedEvent.Raise();
        }
    }

    public void UpdateSticksUI()
    {
        // Update the text on the UI element
        SticksUI.text = "Sticks: " + NumberOfSticks + " / " + TotalSticksNeeded;
    }
}
