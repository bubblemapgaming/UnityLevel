using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameTimerScript : MonoBehaviour {

    [SerializeField]
    private Text TimerText;

    [SerializeField]
    private GameObject PenaltyText;

    public static float elapsedTime;
    private static float totalPenaltyTime;

    //UI Display Vars
    private static string displayMiliseconds;
    private static string displaySeconds;
    private static string displayMinutes;

    //Player Object Var
    private GameObject playerObject;

    //Event for timer penalty
    public delegate void PenaltyAction(float penaltyTime);
    public static PenaltyAction ApplyTimePenalty;

    private void OnEnable()
    {
        ApplyTimePenalty += AddPenaltyTime;
    }

    private void OnDisable()
    {
        ApplyTimePenalty -= AddPenaltyTime;
    }


    // Use this for initialization
    void Start () {
        //Start the Timer String Empty
        TimerText.text = string.Empty;

        //Set all time variables to 0
        elapsedTime = 0.0f;

        //Set Player Object
        playerObject = GameObject.Find("FPSController");
    }
	
	// Update is called once per frame
	void Update () {
        if(playerObject.GetComponent<CharacterConditions>().isEscaping == true) {
            elapsedTime += Time.deltaTime;

            //Format UI Display
            TimerText.text = FormatTimeString(elapsedTime);

        }


    }
    
    /// <summary>
    /// Takes an input of seconds and turns it in to formmated string (00:00:00)
    /// </summary>
    /// <param name="a_elapsedSeconds">Number of seconds to convert</param>
    /// <returns>String of Time (00:00:00)</returns>
    public static string FormatTimeString(float a_elapsedSeconds)
    {
        //Calcuate Minutes/Seconds/Milliseconds
        float timeMinutes = Mathf.FloorToInt((a_elapsedSeconds) / 60);
        float timeSeconds = Mathf.FloorToInt((a_elapsedSeconds) % 60);
        float timeMiliseconds = (float)Math.Floor((a_elapsedSeconds - Math.Truncate(a_elapsedSeconds)) * 100);

        //Format Time String
        displayMinutes = FormatTimeSection(timeMinutes);
        displaySeconds = FormatTimeSection(timeSeconds);
        displayMiliseconds = FormatTimeSection(timeMiliseconds);

        return displayMinutes + ":" + displaySeconds + ":" + displayMiliseconds;
    }

    /// <summary>
    /// Formats time strings to have leading zeros if they are less than 9. Returns String.
    /// </summary>
    /// <param name="a_inputTime">Time to convert</param>
    /// <returns></returns>
    private static string FormatTimeSection(float a_inputTime)
    {
        if (a_inputTime < 10.0f)
        {
            return "0" + a_inputTime.ToString();
        }
        else
        {
            return a_inputTime.ToString();
        }
    }


    /// <summary>
    /// Applies a time penalty to the player of the time speicified, trigger penalty event
    /// </summary>
    /// <param name="a_penaltyTime">Time to Penalise by</param>
    public void AddPenaltyTime(float a_penaltyTime)
    {
        elapsedTime += a_penaltyTime;
        totalPenaltyTime += a_penaltyTime;
    }

    /// <summary>
    /// Returns the total elapsed time in the timer in a formated way
    /// </summary>
    /// <returns>Total Elasped Time formatted (00:00:00)</returns>
    public static string GetFormattedElapsedTime()
    {
        return(displayMinutes + ":" + displaySeconds + ":" + displayMiliseconds);
    }

    /// <summary>
    /// Returns the total penalty time in the timer in a formated way
    /// </summary>
    /// <returns>Total Penalty Time formatted (00:00:00)</returns>
    public static string GetFormattedPenaltyTime()
    {
        return (FormatTimeString(totalPenaltyTime));
    }

}
