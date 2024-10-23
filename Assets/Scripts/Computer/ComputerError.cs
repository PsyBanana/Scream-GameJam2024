using UnityEngine;
using TMPro;
using ScriptableObjectArchitecture;

public class ComputerError : MonoBehaviour, IGameEventListener
{
    [SerializeField] GameEvent _onAllSticksCollectedEvent = default(GameEvent);

    TextMeshProUGUI _errorText;

    ComputerInteractable _computerInteractable;

    private void Awake()
    {
        _errorText = GetComponent<TextMeshProUGUI>();
        _errorText.enabled = false;
    }

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

        _errorText.enabled = true;
        _errorText.text = message;

        Invoke(nameof(ExitComputerView), 2.5f);
    }

    void ExitComputerView()
    {
        _computerInteractable.InteractStart();
    }

    private void OnEnable()
    {
        _onAllSticksCollectedEvent.AddListener(this);
    }

    public void OnEventRaised()
    {
        DisplayErrorMessage("No memory. Insert memory card.");
    }

    private void OnDisable()
    {
        _onAllSticksCollectedEvent.RemoveListener(this);
    }
}
