using UnityEngine;

/// <summary>
/// Serializable container representing a named audio clip used by the AudioManager.
/// </summary>
[System.Serializable]
public class Sound
{
    /// <summary>
    /// Identifier for the sound. Should be unique within the relevant collection.
    /// </summary>
    public string Name;

    /// <summary>
    /// The audio clip associated with this sound.
    /// </summary>
    public AudioClip Clip;
}
