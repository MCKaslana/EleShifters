using System;
using UnityEngine;

public class ReactionManager : Singleton<ReactionManager>
{
    public void React(ElementData playerElement, ElementData opponentElement)
    {
        if (playerElement.strongAgainst == opponentElement)
        {
            Debug.Log($"{playerElement.elementType} beats {opponentElement.elementType}");
            //Update by adding score
        }
        else if (playerElement.weakAgainst == opponentElement)
            Debug.Log($"{playerElement.elementType} is beaten by {opponentElement.elementType}");
        else
            Debug.Log($"{playerElement.elementType} and {opponentElement.elementType} are neutral.");
    }
}
