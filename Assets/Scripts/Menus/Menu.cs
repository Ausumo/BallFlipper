using UnityEngine;

/// <summary>
/// Small helper representing an in-game menu. Keeps track of open state and
/// exposes a read-only name for other controllers.
/// </summary>
public class Menu : MonoBehaviour
{
    [SerializeField]
    private string menuName;

    [SerializeField]
    private bool isOpen;

    /// <summary>
    /// The assigned name of this menu (used by MenuManager to open/close by name).
    /// </summary>
    public string MenuName => menuName;

    /// <summary>
    /// Whether the menu is currently open.
    /// </summary>
    public bool IsOpen => isOpen;

    /// <summary>
    /// Opens the menu and sets the internal state.
    /// </summary>
    public void Open()
    {
        isOpen = true;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Closes the menu and sets the internal state.
    /// </summary>
    public void Close()
    {
        isOpen = false;
        gameObject.SetActive(false);
    }
}

