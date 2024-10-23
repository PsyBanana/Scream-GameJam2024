using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] string _uniqueID;

    private void Start()
    {
        LoadObjectState();
    }

    public void SaveObjectState()
    {
        bool isActive = gameObject.activeSelf;
        ES3.Save(_uniqueID + "_Axe2DActiveState", isActive);
    }

    public void LoadObjectState()
    {
        if (ES3.KeyExists(_uniqueID + "_Axe2DActiveState"))
        {
            bool loadedState = ES3.Load<bool>(_uniqueID + "_Axe2DActiveState");
            gameObject.SetActive(loadedState);
        }
    }

    private void OnDestroy()
    {
        SaveObjectState();
    }
}
