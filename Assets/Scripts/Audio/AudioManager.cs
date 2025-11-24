using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    //Sound arrays for music and sound effects
    [Tooltip("Background music sounds collection")]
    public Sound[] MusicSounds;

    [Tooltip("Gameplay / UI sound effects collection")]
    public Sound[] SfxSounds;

    //Audio sources for playing music and sound effects
    [Tooltip("Audio source used for background music (single source)")]
    public AudioSource MusicSource;

    [Tooltip("Audio source used for one-shot sound effects")]
    public AudioSource SfxSource;

    //Mixer settings
    [Header("Mixer")]
    [SerializeField]
    private AudioMixer masterMixer;

    // Mixer exposed parameters expect decibel values. We store normalized 0..1 values here.
    [Range(0.0001f, 1f)]
    public float MasterVolume = 1f;

    [Range(0.0001f, 1f)]
    public float MusicVolume = 1f;

    [Range(0.0001f, 1f)]
    public float SoundsVolume = 1f;

    public static AudioManager Instance { get; private set; }

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

    /// <summary>
    /// Plays a background music by name. Replaces the clip on the music source.
    /// </summary>
    public void PlayMusic(string name)
    {
        var music = Array.Find(MusicSounds, m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));

        if (music == null || music.Clip == null)
        {
            Debug.LogWarning($"Music '{name}' not found or has no clip.");
            return;
        }

        MusicSource.clip = music.Clip;
        MusicSource.Play();
    }

    /// <summary>
    /// Stops the currently playing music (if the named clip matches the current clip).
    /// If the name does not match the current clip, still stops the music.
    /// </summary>
    public void StopMusic(string name = null)
    {
        if (!string.IsNullOrEmpty(name))
        {
            var music = Array.Find(MusicSounds, m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
            if (music == null)
            {
                Debug.LogWarning($"Music '{name}' not found.");
                return;
            }
        }

        MusicSource.Stop();
    }

    /// <summary>
    /// Plays a one-shot sound effect by name.
    /// </summary>
    public void PlaySound(string name)
    {
        var sound = Array.Find(SfxSounds, s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));

        if (sound == null || sound.Clip == null)
        {
            Debug.LogWarning($"Sound '{name}' not found or has no clip.");
            return;
        }

        SfxSource.PlayOneShot(sound.Clip);
    }

    /// <summary>
    /// Stops the sound source playback. This only stops the clip currently assigned to the SFX source.
    /// </summary>
    public void StopSound(string name = null)
    {
        if (!string.IsNullOrEmpty(name))
        {
            var sound = Array.Find(SfxSounds, s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
            if (sound == null)
            {
                Debug.LogWarning($"Sound '{name}' not found.");
                return;
            }
        }

        SfxSource.Stop();
    }

    /// <summary>
    /// Applies the current normalized volumes to the AudioMixer in decibels.
    /// Uses a small lower bound to avoid Log10(0) errors.
    /// </summary>
    public void ApplyVolumes()
    {
        // Clamp values to valid range and avoid zero
        MasterVolume = Mathf.Clamp(MasterVolume, 0.0001f, 1f);
        MusicVolume = Mathf.Clamp(MusicVolume, 0.0001f, 1f);
        SoundsVolume = Mathf.Clamp(SoundsVolume, 0.0001f, 1f);

        if (masterMixer == null)
        {
            Debug.LogWarning("Master mixer reference is missing. Cannot apply volumes.");
            return;
        }

        masterMixer.SetFloat("MasterVolume", Mathf.Log10(MasterVolume) * 20f);
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(MusicVolume) * 20f);
        masterMixer.SetFloat("SoundsVolume", Mathf.Log10(SoundsVolume) * 20f);
    }
}

