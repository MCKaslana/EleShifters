using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("< Volume Settings >")]
    [Range(0f, 1f)][SerializeField] private float masterVolume = 1f;
    [Range(0f, 1f)][SerializeField] private float musicVolume = 1f;
    [SerializeField] private float fadeDuration = 1.5f;

    [Header("< Audio Sources >")]
    [SerializeField] private AudioSource mainMenuMusic;
    [SerializeField] private AudioSource gameplayMusic;

    [Header("< Scene Settings >")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private AudioSource currentTrack;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == mainMenuSceneName)
            PlayTrack(mainMenuMusic);
        else
            PlayTrack(gameplayMusic);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == mainMenuSceneName)
            SwitchTo(mainMenuMusic);
        else
            SwitchTo(gameplayMusic);
    }

    private void PlayTrack(AudioSource track)
    {
        currentTrack = track;
        currentTrack.volume = masterVolume * musicVolume;
        currentTrack.Play();
    }

    private void SwitchTo(AudioSource newTrack)
    {
        if (currentTrack == newTrack)
            return;

        StopAllCoroutines();
        StartCoroutine(FadeTracks(currentTrack, newTrack, fadeDuration));
        currentTrack = newTrack;
    }

    private IEnumerator FadeTracks(AudioSource previousAudio, AudioSource currentAudio, float duration)
    {
        float time = 0f;
        float targetVolume = masterVolume * musicVolume;

        currentAudio.volume = 0f;
        currentAudio.Play();

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            if (previousAudio != null)
                previousAudio.volume = Mathf.Lerp(targetVolume, 0f, t);

            currentAudio.volume = Mathf.Lerp(0f, targetVolume, t);
            yield return null;
        }

        if (previousAudio != null)
            previousAudio.Stop();

        currentAudio.volume = targetVolume;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
