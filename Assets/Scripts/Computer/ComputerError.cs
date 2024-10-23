using UnityEngine;
using TMPro;
using ScriptableObjectArchitecture;

public class ComputerError : MonoBehaviour, IGameEventListener
{
    [SerializeField] GameEvent _onAllSticksCollectedEvent = default(GameEvent);

    TextMeshProUGUI _errorText;

    Player3D _player3D;
    ComputerInteractable _computerInteractable;

    private void Awake()
    {
        _errorText = GetComponent<TextMeshProUGUI>();
        _errorText.enabled = false;
    }

    private void Start()
    {
        _player3D = FindAnyObjectByType<Player3D>();
        _computerInteractable = FindAnyObjectByType<ComputerInteractable>();
    }

    public void DisplayErrorMessage(string message)
    {
        if (!_errorText || !_player3D || !_computerInteractable)
        {
            return;
        }

        _errorText.enabled = true;
        _errorText.text = message;

        Invoke(nameof(ExitComputerView), 2.5f);

        UIManager.Get<UIFade>().Fade(0f, 1f, 3f);
    }

    void ExitComputerView()
    {
        _computerInteractable.InteractStart();

        _player3D.TeleportPlayerTo(new Vector3(-180f, 0f, -250f));

        UIManager.Get<UIFade>().Fade(1f, 0f, 0.5f);
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
