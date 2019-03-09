using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EscapeLightController : MonoBehaviour {

    private GameObject[] lightObjects;

    [SerializeField]
    private float flashInterval = 2;

    private float flashTimer;
    private GameObject playerObject;

    //Event for activate/deactivate light
    public delegate void LightEvent();
    public static LightEvent toggleLights;


	// Use this for initialization
	void Start () {

        //Get Player Object
        playerObject = GameObject.Find("FPSController");

        //Set Flash Timer to Interval
        flashTimer = flashInterval;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //If the player has initiated thier escape
        if(playerObject.GetComponent<CharacterConditions>().isEscaping == true) { 
            flashTimer -= Time.deltaTime;

            //If timer has expired disable the light if it is on and vise-versa, creating a flashing effect.
            if (flashTimer <= 0) {
                //Call Event to toggle lights
                toggleLights();
                flashTimer = flashInterval;
            }
        }
    }
}
