using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Responsible for handling menu navigation and UI controls available in menus.
/// Connects UI sliders to the AudioManager and exposes helper methods for buttons.
/// </summary>
public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Menu[] menus;

    public static MenuManager Instance { get; private set; }

    [Header("Audio Sliders")]
    [SerializeField]
    private Slider masterVolumeSlider;

    [SerializeField]
    private Slider musicVolumeSlider;

    [SerializeField]
    private Slider soundsVolumeSlider;

    [SerializeField]
    private TMP_Text highscoreText;

    private AudioManager audioManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Ensure GameManager has loaded values
        GameManager.Instance.LoadUsername();
        GameManager.Instance.LoadHighscore();
        GameManager.Instance.LoadOptions();

        // Cache AudioManager and initialize UI from current audio values
        audioManager = AudioManager.Instance;

        UpdateSlidersFromAudio();

        if (highscoreText != null)
        {
            highscoreText.text = "Highscore: " + GameManager.Instance.Highscore;
        }

        // Start menu music if available
        audioManager?.PlayMusic("menuMusic");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].MenuName == menuName)
            {
                menus[i].Open();
            }
            else if (menus[i].IsOpen)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].IsOpen)
            {
                CloseMenu(menus[i]);
            }
        }

        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    /// <summary>
    /// Updates AudioManager volumes from the slider values and applies them to the mixer.
    /// Should be called by UI slider OnValueChanged events.
    /// </summary>
    public void UpdateVolumeFromSliders()
    {
        if (audioManager == null)
            audioManager = AudioManager.Instance;

        if (audioManager == null) return;

        audioManager.MasterVolume = masterVolumeSlider != null ? masterVolumeSlider.value : audioManager.MasterVolume;
        audioManager.MusicVolume = musicVolumeSlider != null ? musicVolumeSlider.value : audioManager.MusicVolume;
        audioManager.SoundsVolume = soundsVolumeSlider != null ? soundsVolumeSlider.value : audioManager.SoundsVolume;

        audioManager.ApplyVolumes();

        // Persist options whenever sliders change
        GameManager.Instance.SaveOptions();
    }

    /// <summary>
    /// Updates the sliders to reflect the current values in the AudioManager.
    /// </summary>
    public void UpdateSlidersFromAudio()
    {
        if (audioManager == null)
            audioManager = AudioManager.Instance;

        if (audioManager == null) return;

        if (masterVolumeSlider != null)
            masterVolumeSlider.SetValueWithoutNotify(audioManager.MasterVolume);

        if (musicVolumeSlider != null)
            musicVolumeSlider.SetValueWithoutNotify(audioManager.MusicVolume);

        if (soundsVolumeSlider != null)
            soundsVolumeSlider.SetValueWithoutNotify(audioManager.SoundsVolume);
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenTerms()
    {
        Application.OpenURL("https://www.riftstonegames.com/TermsEULA_App_.html");
    }

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("https://www.riftstonegames.com/PrivacyPolicy_App_.html");
    }

    public void PlaySound(string soundName)
    {
        audioManager?.PlaySound(soundName);
    }

    public void PlayMusic(string musicName)
    {
        audioManager?.PlayMusic(musicName);
    }

    public void StopMusic(string musicName)
    {
        audioManager?.StopMusic(musicName);
    }
}
