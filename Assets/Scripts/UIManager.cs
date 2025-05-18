using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else { instance = this; }
    }

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject winMenu;

    public void TogglePauseMenu(bool isActive) => pauseMenu.SetActive(isActive);
    public void ToggleGameOverMenu(bool isActive) => gameOverMenu.SetActive(isActive);
    public void ToggleWinMenu(bool isActive) => winMenu.SetActive(isActive);
}
