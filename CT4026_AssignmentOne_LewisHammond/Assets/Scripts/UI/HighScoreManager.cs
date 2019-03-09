using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HighScoreManager{

    public static string playerName;

    public static bool SaveHighScore(float score)
    { 

        //For each of the 5 highest score
        for (int i = 1; i <= 5; i++) //for top 5 highscores
        {

            //If we are less than a score, as with time lowest is best
            if (PlayerPrefs.GetFloat("highscorePosScore" + i) > score || PlayerPrefs.GetFloat("highscorePosScore" + i) == 0)
            {

                //Get the current high score in postion that we are better than
                float currentHighScoreInPos = PlayerPrefs.GetFloat("highscorePosScore" + i);
                PlayerPrefs.SetFloat("highscorePosScore" + i, score);
                PlayerPrefs.SetString("hightscorePosName" + 1, playerName);

                //Move Scores down
                if(i>5)
                {
                    PlayerPrefs.SetFloat("highscorePosScore" + i + 1, currentHighScoreInPos);
                    PlayerPrefs.SetString("hightscorePosName" + i + 1, playerName);
                }

                return true;
                
            }
        }

        //If we were not in the top 5, it is not a high score
        return false;

    }
}
