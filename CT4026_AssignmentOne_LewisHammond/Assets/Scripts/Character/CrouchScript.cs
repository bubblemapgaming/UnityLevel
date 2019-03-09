using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchScript : MonoBehaviour {

    [SerializeField]
    private KeyCode crouchKey = KeyCode.C;

    [SerializeField]
    private float crouchHeight = 0.2f;
    private float standHeight = 1.8f;

    private CharacterController controller; 
    private bool isCrouching = false;

	// Use this for initialization
	void Start () {
        //Get Character Controller	
        controller = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        //If player has pressed crouch key
        if (Input.GetKeyDown(crouchKey))
        {
            //If we are crouching stop crouching if we are not then start crouching
            if (isCrouching)
            {
                isCrouching = false;
                controller.height = standHeight;
            }
            else
            {
                isCrouching = true;
                controller.height = crouchHeight;
            }
        }

	}
}
