using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreMenuController : MonoBehaviour {

    [SerializeField]
    Text HighscoreText;

    [SerializeField]
    private GameObject landingMenu;
    [SerializeField]
    private GameObject highScoreMenu;

    string HighScoreString;

	// Use this for initialization
	void Start () {
        //Title
        HighScoreString = "<b>NAME - TIME</b>\n";

        //Format different high scores in the string
        for(int i = 0; i <= 5; i++)
        {
            HighScoreString += FormatHighScoreString(PlayerPrefs.GetString("hightscorePosName" + i), PlayerPrefs.GetFloat("highscorePosScore" + i));
        }

        HighscoreText.text = HighScoreString;

            
	}
	
    /// <summary>
    /// Takes the name and time of a high score and converts in to formmated string
    /// </summary>
    /// <param name="a_playerName"></param>
    /// <param name="a_time"></param>
    /// <returns>Formatted High Score String (NAME - 00:00:00)</returns>
	private string FormatHighScoreString(string a_playerName, float a_time)
    {
        //If time = 0 then it should not count as a high score
        if(a_time != 0){ 
            string returnName = a_playerName.ToUpper();
            string returnTime = GameTimerScript.FormatTimeString(a_time);

            return returnName + "  -  " + returnTime + "\n";
        }
        else
        {
            return string.Empty;
        }

    }

    public void ReturnToLandingPage()
    {
        highScoreMenu.SetActive(false);
        landingMenu.SetActive(true);
    }
}
