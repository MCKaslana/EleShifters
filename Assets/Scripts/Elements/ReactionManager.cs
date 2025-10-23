using System;
using UnityEngine;

public class ReactionManager : Singleton<ReactionManager>
{
    [SerializeField] private AudioClip _correctSound;
    [SerializeField] private AudioClip _wrongSound;
    private AudioSource _soundSource;
    protected override bool PersistBetweenScenes => false;

    protected override void Awake()
    {
        base.Awake();
        _soundSource = gameObject.AddComponent<AudioSource>();
    }

    public void React(ElementData playerElement, ElementData opponentElement)
    {
        if (playerElement.strongAgainst == opponentElement)
        {
            GameManager.Instance.AddScore();
            _soundSource.PlayOneShot(_correctSound);
        }
        else
        {
            GameManager.Instance.UpdatePlayerLives();
            _soundSource.PlayOneShot(_wrongSound);
        }
    }
}
