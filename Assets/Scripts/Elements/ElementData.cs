using UnityEngine;

[CreateAssetMenu(fileName = "ElementData", menuName = "Element System/ElementData")]
public class ElementData : ScriptableObject
{
    [Header("Element Info")]
    public ElementTypes elementType;

    [Header("Relationships")]
    public ElementData strongAgainst;
    public ElementData weakAgainst;
}
