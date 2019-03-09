using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndOfGameUI : MonoBehaviour {

    [SerializeField]
    private Text HighScoreText;

    [SerializeField]
    private Text TotalTimeText;

    [SerializeField]
    private Text PenaltyTimeText;

    //Update all UI Values 
    void Start () {

        //Make Cursor Visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Show Total Time
        TotalTimeText.text = GameTimerScript.GetFormattedElapsedTime();

        //Show Penalty Time
        PenaltyTimeText.text = "Of which " + GameTimerScript.GetFormattedPenaltyTime() + " was gained trough penalty time";

        //Do High Score Saving
        if (HighScoreManager.SaveHighScore(GameTimerScript.elapsedTime))
        {
            //If we succesfully saved a new high score inform the user
            HighScoreText.text = "NEW HIGHSCORE! (TOP 5)";
        }
        else
        {
            //Else Leave it empty
            HighScoreText.text = string.Empty;
        }

    }

    /// <summary>
    /// Reloads the game main scene
    /// </summary>
    public void PlayAgain()
    {
        SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// Returns to the game Main Menu
    /// </summary>
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
	
	
}
