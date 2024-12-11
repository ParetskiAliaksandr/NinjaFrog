using TMPro;
using UnityEngine;

public class KeyInfoText : MonoBehaviour
{
    private TMP_Text _keyInfo;

    private void Awake()
    {
        _keyInfo = GetComponent<TMP_Text>();

        _keyInfo.text = "Collected 0 keys out of 3";
    }

    private void OnEnable()
    {
        Pocket.OnAddKeyItems += UpdateKeyInfoText;
    }

    private void UpdateKeyInfoText(int amount)
    {
        _keyInfo.text = $"Collected {amount} keys out of 3";
    }

    private void OnDisable()
    {
        Pocket.OnAddKeyItems -= UpdateKeyInfoText;
    }
}
