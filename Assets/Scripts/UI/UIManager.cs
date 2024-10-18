using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.Events;
using StimpakEssentials;
using System.Collections.Generic;
using System;

public struct UIStackedAction
{
    public string Name;
    public UnityAction Action;
    public bool StopProcessingActions;
}

public class UIManager : SingletonBehaviour<UIManager>
{
    [SerializeField] GameObjectCollection _uiPrefabsCollection = default(GameObjectCollection);

    Dictionary<Type, Component> _cachedUI = new Dictionary<Type, Component>();
    List<UIStackedAction> _actionStack = new List<UIStackedAction>();
    Queue<UIStackedAction> _pendingQueue = new Queue<UIStackedAction>();
    bool _processingEnabled = true;
    bool _processingStack = false;

    private void Start()
    {
        InitializeUIPrefabs();
    }

    public static T Get<T>() where T : Component
    {
        UIManager ui = Instance;
        Type type = typeof(T);

        if (!ui._cachedUI.ContainsKey(type))
        {
            var component = ui.GetComponentInChildren<T>(true);

            if (component)
            {
                ui._cachedUI.Add(type, component);
            }
        }

        ui._cachedUI.TryGetValue(type, out Component value);

        return value as T;
    }

    public static void PushAction(UIStackedAction item)
    {
        UIManager ui = Instance;

        if (!ui._processingStack)
        {
            ui._actionStack.Add(item);
        }
        else
        {
            ui._pendingQueue.Enqueue(item);
        }
    }

    public static void RemoveAction(UIStackedAction item)
    {
        Instance._actionStack.Remove(item);
    }

    public static bool PopAction()
    {
        UIManager ui = Instance;

        if (!ui._processingEnabled)
        {
            return false;
        }

        int processed = 0;
        UIStackedAction item;

        ui._processingStack = true;

        do
        {
            if (ui._actionStack.Count == 0)
            {
                break;
            }

            item = ui._actionStack[ui._actionStack.Count - 1];
            ui._actionStack.RemoveAt(ui._actionStack.Count - 1);

            item.Action?.Invoke();
            ++processed;
        }
        while (!item.StopProcessingActions);

        ui._processingStack = false;

        if (ui._pendingQueue.Count > 0)
        {
            int pending = ui._pendingQueue.Count;

            for (int i = 0; i < pending; ++i)
            {
                ui._actionStack.Add(ui._pendingQueue.Dequeue());
            }
        }

        return processed > 0;
    }

    public static void SetActionProcessingEnabled(bool enabled)
    {
        Instance._processingEnabled = enabled;
    }

    void InitializeUIPrefabs()
    {
        for (int i = 0; i < _uiPrefabsCollection.Count; i++)
        {
            GameObject uiPrefab = _uiPrefabsCollection[i];

            GameObject instantiatedPrefab = Instantiate(uiPrefab, transform);
        }
    }
}
