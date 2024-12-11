using System;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    private List<Key> keyItems = new List<Key>();

    public static event Action<int> OnAddKeyItems;

    public void AddKey(Key key)
    {
        keyItems.Add(key);

        UpdateKeyInfo();
    }

    private void UpdateKeyInfo()
    {
        int amount = keyItems.Count;

        OnAddKeyItems?.Invoke(amount);
    }

    public int GetKeyAmount()
    {
        return keyItems.Count;
    }
}
