using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTriggerDamage : MonoBehaviour {

    [SerializeField]
    private bool enableTimePenalty = true;

    [SerializeField]
    private bool enableDamage = true;

    [SerializeField]
    private float timePenalty = 0.2f;

    [SerializeField]
    private float damageAmount;

    [SerializeField]
    private GameObject timer;

    private float totalPenaltyTime;


    // Use this for initialization
    void Start () {
        totalPenaltyTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //When the player stays in the damage box apply a small time penalty each frame and a small ammount of damage
    //Accumulate the penalty time in to a var that can be applied once the player leaves the trigger to avoid notification spam
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player") { 
            //If Time Penalty is enabled then apply a time penatly to the player
            if (enableTimePenalty)
            {
                totalPenaltyTime += timePenalty;
            }

            //If Time Penalty is enabled then apply a time penatly to the player
            if (enableDamage)
            {
                other.GetComponent<CharacterConditions>().playerHealth -= damageAmount;
            }
        }

    }

    //When the player leaves the trigger, apply the accumulated penalty time and reset it
    void OnTriggerExit(Collider other)
    {
        if (enableTimePenalty) {
            //Call Event to Apply Time Penalty
            GameTimerScript.ApplyTimePenalty(totalPenaltyTime);
            totalPenaltyTime = 0.0f;
        }
    }

}
