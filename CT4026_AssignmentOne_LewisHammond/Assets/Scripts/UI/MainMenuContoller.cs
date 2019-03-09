using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuContoller : MonoBehaviour {

    [SerializeField]
    GameObject landingMenu;
    [SerializeField]
    GameObject highScoreMenu;
    [SerializeField]
    GameObject optionsMenu;
    [SerializeField]
    InputField nameInput;

    /// <summary>
    /// Starts Game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// Loads High Score Menu
    /// </summary>
    public void LoadHighScoreMenu()
    {
        HideLandingMenu();
        highScoreMenu.SetActive(true);
    }

    /// <summary>
    /// Loads the options menu
    /// </summary>
    public void LoadOptionsMenu()
    {
        HideLandingMenu();
        optionsMenu.SetActive(true);
    }

    /// <summary>
    /// Quits the Application
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();

    }
    /// <summary>
    /// Hides the Main "Landing Menu"
    /// </summary>
    private void HideLandingMenu()
    {
        landingMenu.SetActive(false);
    }

    /// <summary>
    /// Sets the player name for highscores
    /// </summary>
    public void SetPlayerName()
    {
        HighScoreManager.playerName = nameInput.text;
    }
}
