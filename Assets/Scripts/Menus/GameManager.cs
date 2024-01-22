using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Highscore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        LoadOptions();
    }

    public void SaveOptions()
    {
        PlayerPrefs.SetFloat("MasterVolume", AudioManager.Instance.masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", AudioManager.Instance.musicVolume);
        PlayerPrefs.SetFloat("SoundVolume", AudioManager.Instance.soundsVolume);

        PlayerPrefs.Save();
    }

    public void LoadOptions()
    {
        AudioManager.Instance.masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        AudioManager.Instance.musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        AudioManager.Instance.soundsVolume = PlayerPrefs.GetFloat("SoundVolume");

        AudioManager.Instance.SetVolumes();
    }

    public void SaveHighscore()
    {
		PlayerPrefs.SetInt("Highscore", Highscore);

		PlayerPrefs.Save();
	}

    public void LoadHighscore()
    {
        Highscore = PlayerPrefs.GetInt("Highscore");
	}
}
