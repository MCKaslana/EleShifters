using UnityEngine;

public class ReactionManager : MonoBehaviour
{
    public void React(ElementData a, ElementData b)
    {
        if (a.strongAgainst == b)
            Debug.Log($"{a.elementType} beats {b.elementType}");
        else if (a.weakAgainst == b)
            Debug.Log($"{a.elementType} is beaten by {b.elementType}");
        else
            Debug.Log($"{a.elementType} and {b.elementType} are neutral.");
    }
}
