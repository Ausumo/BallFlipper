using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public string Username;
    public int Highscore;

    public TMP_InputField enterNameField;

    public Action<string, int> submitScoreEvent;

    private int _firstStart = 0;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _enterNameMenu;

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

	    //https:// danqzq.itch.io/leaderboard-creator
	}

    private void Start()
    {
        _firstStart = PlayerPrefs.GetInt("FirstStart");

		if (_firstStart == 0)
        {
			AudioManager.Instance.masterVolume = .5f;
			AudioManager.Instance.musicVolume = .5f;
			AudioManager.Instance.soundsVolume = .5f;

            SaveOptions();

            _mainMenu.SetActive(false);
            _enterNameMenu.SetActive(true);

            _firstStart = 1;
            PlayerPrefs.SetInt("FirstStart", _firstStart);

			PlayerPrefs.Save();
        }
        else
        {
			_mainMenu.SetActive(true);
			_enterNameMenu.SetActive(false);

			AudioManager.Instance.masterVolume = PlayerPrefs.GetFloat("MasterVolume");
			AudioManager.Instance.musicVolume = PlayerPrefs.GetFloat("MusicVolume");
			AudioManager.Instance.soundsVolume = PlayerPrefs.GetFloat("SoundVolume");
		}

        LoadOptions();
        LoadUsername();
        LoadHighscore();
    }

    public void SetUsername()
    {
		Username = enterNameField.text;
	}

	public void SaveUsername()
    {
		PlayerPrefs.SetString("Username", Username);

		PlayerPrefs.Save();
	}
	public void LoadUsername()
	{
		Username = PlayerPrefs.GetString("Username");
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
		submitScoreEvent?.Invoke(Username, Highscore);
		PlayerPrefs.SetInt("Highscore", Highscore);

		PlayerPrefs.Save();
	}

    public void LoadHighscore()
    {
        Highscore = PlayerPrefs.GetInt("Highscore");
	}
}
