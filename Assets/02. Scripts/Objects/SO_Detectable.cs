using UnityEngine;

public enum InterativeType
{
    Interactable,
    Monster
}

[CreateAssetMenu(menuName = "SO/Data/Interactable")]

public class SO_Detectable : ScriptableObject
{
    public string Name;
    public InterativeType InterativeType;
    public string Desc;
}