using System;
using UnityEngine;

public class ReactionManager : Singleton<ReactionManager>
{
    public void React(ElementData playerElement, ElementData opponentElement)
    {
        if (playerElement.strongAgainst == opponentElement)
        {
            GameManager.Instance.AddScore();
        }
        else if (playerElement.weakAgainst == opponentElement)
        {
            GameManager.Instance.UpdatePlayerLives();
        }
        else
            GameManager.Instance.UpdatePlayerLives();
    }
}
