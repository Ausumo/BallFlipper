using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Small helper representing an in-game menu. Keeps track of open state and
/// exposes a read-only name for other controllers.
/// </summary>
public class Menu : MonoBehaviour
{
    [SerializeField]
    [FormerlySerializedAs("menuName")]
    private string _menuName;

    [SerializeField]
    [FormerlySerializedAs("isOpen")]
    private bool _isOpen;

    /// <summary>
    /// The assigned name of this menu (used by MenuManager to open/close by name).
    /// </summary>
    public string MenuName => _menuName;

    /// <summary>
    /// Whether the menu is currently open.
    /// </summary>
    public bool IsOpen => _isOpen;

    /// <summary>
    /// Opens the menu and sets the internal state.
    /// </summary>
    public void Open()
    {
        _isOpen = true;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Closes the menu and sets the internal state.
    /// </summary>
    public void Close()
    {
        _isOpen = false;
        gameObject.SetActive(false);
    }
}

