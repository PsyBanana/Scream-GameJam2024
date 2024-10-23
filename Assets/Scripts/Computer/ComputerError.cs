using UnityEngine;
using TMPro;

public class ComputerError : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _errorText;

    ComputerInteractable _computerInteractable;

    private void Start()
    {
        _computerInteractable = FindAnyObjectByType<ComputerInteractable>();
    }

    public void DisplayErrorMessage(string message)
    {
        if (!_errorText)
        {
            return;
        }

        _errorText.text = message;

        Invoke(nameof(ExitComputerView), 2.5f);
    }

    void ExitComputerView()
    {
        _computerInteractable.InteractStart();
    }
}
