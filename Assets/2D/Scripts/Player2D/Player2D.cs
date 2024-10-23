using UnityEngine;

public class Player2D : MonoBehaviour
{
    [SerializeField] string _uniqueID;

    Rigidbody Player2DRigidbody { get; set; }
    CollectItems2D _collectItems2D;
    PlayerMovement2D _playerMovement2D;
    
    private void Awake()
    {
        Player2DRigidbody = GetComponent<Rigidbody>();
        _collectItems2D = GetComponent<CollectItems2D>();
        _playerMovement2D = GetComponent<PlayerMovement2D>();
    }

    private void Start()
    {
        LoadPlayer2DData();
    }

    public void SavePlayer2DData()
    {
        Vector3 playerPosition = transform.position;
        int numberOfSticks = _collectItems2D.NumberOfSticks;
        bool isAxeEquipped = _playerMovement2D.isAxeEquipped;
        ES3.Save(_uniqueID + "_Position", playerPosition);
        ES3.Save(_uniqueID + "_NumberOfSticks", numberOfSticks);
        ES3.Save(_uniqueID + "IsAxeEquipped", isAxeEquipped);
    }

    public void LoadPlayer2DData()
    {
        if (ES3.KeyExists(_uniqueID + "_Position") && ES3.KeyExists(_uniqueID + "_NumberOfSticks") && ES3.KeyExists(_uniqueID + "_IsAxeEquipped"))
        {
            LoadPlayerPosition();

            int numberOfSticks = ES3.Load<int>(_uniqueID + "_NumberOfSticks");
            _collectItems2D.NumberOfSticks = numberOfSticks;

            _collectItems2D.UpdateSticksUI();

            bool isAxeEquipped = ES3.Load<bool>(_uniqueID + "_IsAxeEquipped");
            _playerMovement2D.isAxeEquipped = isAxeEquipped;
        }
    }

    public void LoadPlayerPosition()
    {
        Vector3 loadedPosition = ES3.Load<Vector3>(_uniqueID + "_Position");
        Player2DRigidbody.MovePosition(loadedPosition);
    }

    private void OnDestroy()
    {
        SavePlayer2DData();
    }

    public string GetUniqueID()
    {
        return _uniqueID;
    }
}
