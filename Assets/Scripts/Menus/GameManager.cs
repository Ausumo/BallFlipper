using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Global game manager responsible for persistable values like username, highscore and audio options.
/// Implements a simple MonoBehaviour singleton and persists between scenes.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public string Username;
    public int Highscore;

    public TMP_InputField EnterNameField;

    public Action<string, int> SubmitScoreEvent;

    private const string FirstStartKey = "FirstStart";
    private const string UsernameKey = "Username";
    private const string HighscoreKey = "Highscore";
    private const string MasterVolumeKey = "MasterVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string SoundsVolumeKey = "SoundsVolume";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        int firstStart = PlayerPrefs.GetInt(FirstStartKey);

        if (firstStart == 0)
        {
            // Set sensible default volumes
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.MasterVolume = 0.5f;
                AudioManager.Instance.MusicVolume = 0.5f;
                AudioManager.Instance.SoundsVolume = 0.5f;

                AudioManager.Instance.ApplyVolumes();
            }

            // Ensure the first start flag is set
            PlayerPrefs.SetInt(FirstStartKey, 1);
            PlayerPrefs.Save();
        }

        // Load saved options and other data
        LoadOptions();
        LoadUsername();
        LoadHighscore();
    }

    /// <summary>
    /// Sets the username from the input field. Call before saving.
    /// </summary>
    public void SetUsername()
    {
        if (EnterNameField != null)
            Username = EnterNameField.text;
    }

    public void SaveUsername()
    {
        PlayerPrefs.SetString(UsernameKey, Username ?? string.Empty);
        PlayerPrefs.Save();
    }

    public void LoadUsername()
    {
        Username = PlayerPrefs.GetString(UsernameKey, string.Empty);
    }

    public void SaveOptions()
    {
        if (AudioManager.Instance != null)
        {
            PlayerPrefs.SetFloat(MasterVolumeKey, AudioManager.Instance.MasterVolume);
            PlayerPrefs.SetFloat(MusicVolumeKey, AudioManager.Instance.MusicVolume);
            PlayerPrefs.SetFloat(SoundsVolumeKey, AudioManager.Instance.SoundsVolume);
            PlayerPrefs.Save();
        }
    }

    public void LoadOptions()
    {
        if (AudioManager.Instance == null) return;

        // Provide defaults if keys are missing
        AudioManager.Instance.MasterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, AudioManager.Instance.MasterVolume);
        AudioManager.Instance.MusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, AudioManager.Instance.MusicVolume);
        AudioManager.Instance.SoundsVolume = PlayerPrefs.GetFloat(SoundsVolumeKey, AudioManager.Instance.SoundsVolume);

        AudioManager.Instance.ApplyVolumes();
    }

    public void SaveHighscore()
    {
        SubmitScoreEvent?.Invoke(Username, Highscore);
        PlayerPrefs.SetInt(HighscoreKey, Highscore);
        PlayerPrefs.Save();
    }

    public void LoadHighscore()
    {
        Highscore = PlayerPrefs.GetInt(HighscoreKey, 0);
    }
}
