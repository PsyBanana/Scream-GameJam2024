using UnityEngine;

public class Player2D : MonoBehaviour
{
    [SerializeField] string _uniqueID;

    Rigidbody Player2DRigidbody { get; set; }
    CollectItems2D _collectItems2D;
    
    private void Awake()
    {
        Player2DRigidbody = GetComponent<Rigidbody>();
        _collectItems2D = GetComponent<CollectItems2D>();
    }

    private void Start()
    {
        LoadPlayer2DData();
    }

    public void SavePlayer2DData()
    {
        Vector3 playerPosition = transform.position;
        int numberOfSticks = _collectItems2D.NumberOfSticks;
        ES3.Save(_uniqueID + "_Position", playerPosition);
        ES3.Save(_uniqueID + "_NumberOfSticks", numberOfSticks);
    }

    public void LoadPlayer2DData()
    {
        if (ES3.KeyExists(_uniqueID + "_Position") && ES3.KeyExists(_uniqueID + "_NumberOfSticks"))
        {
            LoadPlayerPosition();

            int numberOfSticks = ES3.Load<int>(_uniqueID + "_NumberOfSticks");
            _collectItems2D.NumberOfSticks = numberOfSticks;

            _collectItems2D.UpdateSticksUI();
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
