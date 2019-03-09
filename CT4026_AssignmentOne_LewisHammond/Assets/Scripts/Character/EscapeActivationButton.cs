using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeActivationButton : Interactable {

    [SerializeField] 
    private GameObject buttonComponent;

    private GameObject playerObject;

    private const float buttonMoveAmount = 0.2f;

    [SerializeField]
    private const float interactionDistance = 2.0f;

    //Button timer to "unpush" button (in seconds)
    private float buttonDeactivateTime = 0.5f;
    private float buttonTimer;

    private Vector3 buttonStartPos;
    private Vector3 buttonPressedPos;

    //Text that shows "Begin Escape"
    [SerializeField]
    private GameObject UIText;

    //Intalise PlayerObject because awake is called after all objects are intalised
    void Awake()
    {
        playerObject = GameObject.Find("FPSController");
    }

    // Use this for initialization
    void Start () {

        //Setup Button Start Postion and initalise playerObject variable
        buttonStartPos = buttonComponent.transform.localPosition;
        buttonPressedPos = buttonStartPos + new Vector3(0, -buttonMoveAmount, 0);
        playerObject = GameObject.Find("FPSController");

        //Button Timer Initalistation (for reseting button state)
        buttonTimer = buttonDeactivateTime;
    }
	
	// Update is called once per frame
	void Update () {

        //Activate Player Escape
        if (Input.GetKeyDown("f") && PlayerInRange(interactionDistance, playerObject))
        {
            buttonComponent.transform.localPosition = buttonPressedPos;
            buttonTimer = buttonDeactivateTime;
            playerObject.GetComponent<CharacterConditions>().isEscaping = true;
        }

        
        //Draw Button Help Text
        if(PlayerInRange(interactionDistance, playerObject))
        {
            if(playerObject.GetComponent<CharacterConditions>().isEscaping == true)
            {
                //Show ESCAPE INITATED
                UIText.GetComponent<TextMesh>().color = Color.red;
                UIText.GetComponent<TextMesh>().text = "ESCAPE INITIATED";
            }
            else
            {
                //Show Press to Escape
                UIText.GetComponent<TextMesh>().color = Color.white;
                UIText.GetComponent<TextMesh>().text = "Activate Escape Protcol";
            }
        }
        else
        {
            //We are not in range so show nothing (empty string)
            UIText.GetComponent<TextMesh>().color = Color.white;
            UIText.GetComponent<TextMesh>().text = string.Empty;

        }

        //Countdown to "un-push button"
        if(buttonTimer <= 0)
        {
            //Reset button postion
            buttonComponent.transform.localPosition = buttonStartPos;
        }
        else
        {
            //reduce button time
            buttonTimer -= Time.deltaTime;
        }
		
	}
}
