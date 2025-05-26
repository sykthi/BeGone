using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public static bool IsGameEnded = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;

    TMP_Text FinalScore;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !IsGameEnded)
        {
            if (IsGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if(IsGameEnded == true)
        {
            if (FinalScore == null)
            {
                FinalScore = gameOverUI.GetComponentInChildren<TMP_Text>();
                FinalScore.text = "Your Score: " + GameManager.score.ToString();
            }
            gameOverUI.SetActive(true);
        }
        else
        {
            gameOverUI.SetActive(false);
        }
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void ResumeGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        Time.timeScale = 1f;
        IsGamePaused = false;
    }
    public void ReloadScene()
    {
        IsGameEnded = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 0");
    }

    public void LoadMainMenu()
    {
        //Time.timeScale = 1f; // Ensure time is resumed when loading main menu
        //SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
