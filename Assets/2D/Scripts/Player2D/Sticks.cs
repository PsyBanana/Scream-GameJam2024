using System;
using UnityEngine;

public class Sticks : MonoBehaviour
{
    [SerializeField] string _uniqueID;

    private void Awake()
    {
        if (string.IsNullOrEmpty(_uniqueID))
        {
            GenerateNewID();
        }
    }

    private void Start()
    {
        LoadObjectState();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colided");
        CollectItems2D CollectItems = other.GetComponent<CollectItems2D>();
        Debug.Log("Found the Collect SCript");

        if (CollectItems != null)
        {
            Debug.Log("Found the Collect Script on: " + other.name); 
            CollectItems.StickCollected();
            SaveObjectState();
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("CollectItems2D script not found on: " + other.name);
        }
    }

    public void SaveObjectState()
    {
        bool isCollected = !gameObject.activeSelf;
        ES3.Save(_uniqueID, isCollected);
    }

    public void LoadObjectState()
    {
        if (ES3.KeyExists(_uniqueID))
        {
            bool loadedState = ES3.Load<bool>(_uniqueID);
            gameObject.SetActive(loadedState);
        }
    }

    [ContextMenu("Generate New ID")]
    public void GenerateNewID()
    {
        _uniqueID = Guid.NewGuid().ToString();
    }
}
