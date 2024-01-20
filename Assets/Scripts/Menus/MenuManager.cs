using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Menu[] _menus;

     public static MenuManager Instance { get; private set; }

    //Master Volume Slider
    [SerializeField]
    private Slider _masterVolumeSlider;

    //Music Volume Slider
    [SerializeField]
    private Slider _musicVolumeSlider;

    //Master Volume Slider
    [SerializeField]
    private Slider _soundsVolumeSlider;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.LoadOptions();
        UpdateSliderFromVolume();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i].menuName == menuName)
            {
                _menus[i].Open();
            }
            else if (_menus[i].isOpen)
            {
                CloseMenu(_menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i].isOpen)
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
    /// Updated die Volumes von den Slidern
    /// </summary>
    public void UpdateVolumeFromSlider()
    {
        AudioManager.Instance.masterVolume = _masterVolumeSlider.value;
        AudioManager.Instance.musicVolume = _musicVolumeSlider.value;
        AudioManager.Instance.soundsVolume = _soundsVolumeSlider.value;
        AudioManager.Instance.SetVolumes();
    }

    /// <summary>
    /// Updated den Slider von den Volumes
    /// </summary>
    public void UpdateSliderFromVolume()
    {
        //Nur die erste Zeile wird ausgef?hrt. Nur wenn ich es davor in extra eigene Variablen abspeichere geht es
        float masterVolume = 0;
        float musicVolume = 0;
        float soundVolume = 0;

        masterVolume = AudioManager.Instance.masterVolume;
        musicVolume = AudioManager.Instance.musicVolume;
        soundVolume = AudioManager.Instance.soundsVolume;

        _masterVolumeSlider.value = masterVolume;
        _musicVolumeSlider.value = musicVolume;
        _soundsVolumeSlider.value = soundVolume;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
