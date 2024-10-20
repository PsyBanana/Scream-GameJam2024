using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public Dictionary<string, object> Data = new Dictionary<string, object>();

    public void SetData(string key, object value)
    {
        if (Data.ContainsKey(key))
        {
            Data[key] = value;
        }
        else
        {
            Data.Add(key, value);
        }
    }

    public T GetData<T>(string key, T defaultValue = default(T))
    {
        if (Data.ContainsKey(key) && Data[key] is T)
        {
            return (T)Data[key];
        }

        return defaultValue;
    }
}
