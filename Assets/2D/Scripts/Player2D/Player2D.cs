using UnityEngine;

public class Player2D : MonoBehaviour
{
    [SerializeField] string _uniqueID;

    Rigidbody _player2DRigidbody;
    CollectItems2D _collectItems2D;
    SpriteRenderer _player2DSpriteRenderer;
    
    private void Awake()
    {
        _player2DRigidbody = GetComponent<Rigidbody>();
        _collectItems2D = GetComponent<CollectItems2D>();
        _player2DSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        LoadPlayer2DData();
    }

    public void SavePlayer2DData()
    {
        Vector3 playerPosition = transform.position;
        int numberOfSticks = _collectItems2D.NumberOfSticks;
        Sprite sprite = _player2DSpriteRenderer.sprite;
        ES3.Save(_uniqueID + "_Position", playerPosition);
        ES3.Save(_uniqueID + "_NumberOfSticks", numberOfSticks);
        ES3.Save(_uniqueID + "_Sprite", sprite);
    }

    public void LoadPlayer2DData()
    {
        if (ES3.KeyExists(_uniqueID + "_Position") && ES3.KeyExists(_uniqueID + "_NumberOfSticks"))
        {
            Vector3 loadedPosition = ES3.Load<Vector3>(_uniqueID + "_Position");
            int numberOfSticks = ES3.Load<int>(_uniqueID + "_NumberOfSticks");
            Sprite loadedSprite = ES3.Load<Sprite>(_uniqueID + "_Sprite");
            _player2DRigidbody.MovePosition(loadedPosition);
            _collectItems2D.NumberOfSticks = numberOfSticks;
            _player2DSpriteRenderer.sprite = loadedSprite;

            _collectItems2D.UpdateSticksUI();
        }
    }

    private void OnDestroy()
    {
        SavePlayer2DData();
    }
}
