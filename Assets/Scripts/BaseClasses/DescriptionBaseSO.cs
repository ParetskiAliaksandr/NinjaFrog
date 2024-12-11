using UnityEngine;

public class DescriptionBaseSO : SerializableScriptableObject
{
    [TextArea] 
    [SerializeField] private string description;
}
