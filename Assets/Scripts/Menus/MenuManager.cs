using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

/// <summary>
/// Responsible for handling menu navigation and UI controls available in menus.
/// Connects UI sliders to the AudioManager and exposes helper methods for buttons.
/// </summary>
public class MenuManager : MonoBehaviour
{
    [SerializeField]
    [FormerlySerializedAs("menus")]
    private Menu[] _menus;

    public static MenuManager Instance { get; private set; }

    [Header("Audio Sliders")]
    [SerializeField]
    [FormerlySerializedAs("masterVolumeSlider")]
    private Slider _masterVolumeSlider;

    [SerializeField]
    [FormerlySerializedAs("musicVolumeSlider")]
    private Slider _musicVolumeSlider;

    [SerializeField]
    [FormerlySerializedAs("soundsVolumeSlider")]
    private Slider _soundsVolumeSlider;

    [SerializeField]
    [FormerlySerializedAs("highscoreText")]
    private TMP_Text _highscoreText;

    private AudioManager _audioManager;

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
        _audioManager = AudioManager.Instance;

        UpdateSlidersFromAudio();

        if (_highscoreText != null)
        {
            _highscoreText.text = "Highscore: " + GameManager.Instance.Highscore;
        }

        // Start menu music if available
        _audioManager?.PlayMusic("menuMusic");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i].MenuName == menuName)
            {
                _menus[i].Open();
            }
            else if (_menus[i].IsOpen)
            {
                CloseMenu(_menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i].IsOpen)
            {
                CloseMenu(_menus[i]);
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
        if (_audioManager == null)
            _audioManager = AudioManager.Instance;

        if (_audioManager == null) return;

        _audioManager.MasterVolume = _masterVolumeSlider != null ? _masterVolumeSlider.value : _audioManager.MasterVolume;
        _audioManager.MusicVolume = _musicVolumeSlider != null ? _musicVolumeSlider.value : _audioManager.MusicVolume;
        _audioManager.SoundsVolume = _soundsVolumeSlider != null ? _soundsVolumeSlider.value : _audioManager.SoundsVolume;

        _audioManager.ApplyVolumes();

        // Persist options whenever sliders change
        GameManager.Instance.SaveOptions();
    }

    /// <summary>
    /// Updates the sliders to reflect the current values in the AudioManager.
    /// </summary>
    public void UpdateSlidersFromAudio()
    {
        if (_audioManager == null)
            _audioManager = AudioManager.Instance;

        if (_audioManager == null) return;

        if (_masterVolumeSlider != null)
            _masterVolumeSlider.SetValueWithoutNotify(_audioManager.MasterVolume);

        if (_musicVolumeSlider != null)
            _musicVolumeSlider.SetValueWithoutNotify(_audioManager.MusicVolume);

        if (_soundsVolumeSlider != null)
            _soundsVolumeSlider.SetValueWithoutNotify(_audioManager.SoundsVolume);
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
        _audioManager?.PlaySound(soundName);
    }

    public void PlayMusic(string musicName)
    {
        _audioManager?.PlayMusic(musicName);
    }

    public void StopMusic(string musicName)
    {
        _audioManager?.StopMusic(musicName);
    }
}
