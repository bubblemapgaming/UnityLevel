using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PenaltyPopup : MonoBehaviour {

    [SerializeField]
    private Text penaltyText;

    //Timer for hiding penalty popup text
    private float penaltyHideTimer;

    //Bool for if the popup is showing
    private bool popUpShowing = false;


    //Subscibes/Unsubscribes for Penalty Events
    private void OnEnable()
    {
        GameTimerScript.ApplyTimePenalty += TriggerPenaltyPopup;
    }

    private void OnDisable()
    {
        GameTimerScript.ApplyTimePenalty -= TriggerPenaltyPopup;
    }

    // Use this for initialization
    void Start () {
        //Start the Penalty String Empty
        penaltyText.text = string.Empty;
        penaltyHideTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        //Check if penalty hide timer is less than zero and penalty text is currently showing then hide the popup, else reduced the timer
        if(popUpShowing && penaltyHideTimer < 0.0f)
        {
            HidePenaltyPopup();
        }else if (popUpShowing)
        {
            penaltyHideTimer -= Time.deltaTime;
        }
	}

    /// <summary>
    /// Function triggers the popup of penalty timer
    /// </summary>
    /// <param name="a_penaltyTime"></param>
    public void TriggerPenaltyPopup(float a_penaltyTime)
    {
        //Setup Text
        penaltyText.text = "TIME PENALTY +" + Math.Round(a_penaltyTime,2) + "s";
        penaltyHideTimer = 5.0f;
        popUpShowing = true;
    }

    private void HidePenaltyPopup()
    {
        penaltyText.text = string.Empty;
        popUpShowing = false;
    }
}
