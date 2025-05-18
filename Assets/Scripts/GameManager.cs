using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
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

    private bool playing;
    private int winCondition;
    private int enemiesDestroied;

    private void Start()
    {
        PauseGame();
        SetWinCondition();
    }

    private void Update()
    {
        if (!playing) { return; }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            UIManager.Instance.TogglePauseMenu(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        playing = true;
    }

    public void PauseGame()
    {
        playing = false;
        Time.timeScale = 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        PauseGame();
        UIManager.Instance.ToggleGameOverMenu(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinGame()
    {
        PauseGame();
        UIManager.Instance.ToggleWinMenu(true);
    }

    public void EnemyKilled() 
    { 
        enemiesDestroied++;
        if (enemiesDestroied == winCondition) { WinGame(); }
    }

    public bool Playing() { return playing; }

    private void SetWinCondition()
    {
        winCondition = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void KillEnemy()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");
        if (list.Length > 0) { list[Random.Range(0, list.Length)].GetComponent<Enemy>().Die(); }
    }
}
