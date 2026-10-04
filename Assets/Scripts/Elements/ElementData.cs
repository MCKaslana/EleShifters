using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ElementData", menuName = "Element System/ElementData")]
public class ElementData : ScriptableObject
{
    [Header("Element Info")]
    public ElementTypes elementType;

    [Header("Relationships")]
    public ElementData strongAgainst;
    public ElementData weakAgainst;

    [Header("Visuals")]
    public Color elementColor;
    public Sprite selectorSprite;
    public GameObject elementPrefab;
}