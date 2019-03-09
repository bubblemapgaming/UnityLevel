using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerScript : Interactable {

    [SerializeField]
    private GameObject door;

    //enum for Close or open
    private enum triggerOptions
    {
        Open, Close
    }

    [SerializeField]
    private triggerOptions triggerType;

    private Vector3 doorTargetPosition;
    private Vector3 doorStartPosition;  

    private float doorHeight;

    private GameObject doorUIText;

    // Use this for initialization
    void Start () {

        //Initalise Variables to get the current Position of the door and use to target door position
        doorStartPosition = door.transform.position;
        doorTargetPosition = doorStartPosition;


        //Work out the door size, used later for working out where the doors target is
        Vector3 doorSize = door.GetComponent<Collider>().bounds.size;
        doorHeight = doorSize.y;

    }

    private void Update()
    {
    }


    void OnTriggerEnter(Collider other)
    {
        //Check that it is the player that has collided then trigger door open and start the timer to auto close the door
        if(other.gameObject.tag == "Player")
        {
            //Check Escape Initalistation Status, if we have not initated the escape, inform the player
            if (other.GetComponent<CharacterConditions>().isEscaping == true)
            {
                if (triggerType == triggerOptions.Open)
                {
                    TriggerDoorOpen();
                    door.GetComponent<PlayerTriggeredDoor>().ResetDoorTimer();
                }
                else if (triggerType == triggerOptions.Close)
                {
                    TriggerDoorClose();
                }
            }
            else
            {
                //Create UI to inform the player to initalise escape
                doorUIText = Create3DText("You must first trigger your escape", door.GetComponent<PlayerTriggeredDoor>().doorUITextPrefab, transform.position + new Vector3(-0.7f, 1.2f, 0), new Vector3(-90, 0, 0));
            }
        }

    }

    //Exit Trigger
    void OnTriggerExit(Collider other)
    {
        //If player has left trigger
        if (other.gameObject.tag == "Player")
        {
            //Destroy 3D Text
            if(doorUIText != null)
            {
                Destroy(doorUIText);
            }
        }
    }

    /// <summary>
    /// Triggers door to begin opening
    /// </summary>
    private void TriggerDoorOpen()
    {

        //Set the door target postion to the start position plus the width in Z Direction
        doorTargetPosition = doorStartPosition + new Vector3(0, doorHeight, 0);

        //Trigger Script to move open door
        door.GetComponent<PlayerTriggeredDoor>().OpenDoor(doorTargetPosition);

    }

    /// <summary>
    /// Triggers door to begin closing
    /// </summary>
    private void TriggerDoorClose()
    {
        //Trigger Door Close Script
        door.GetComponent<PlayerTriggeredDoor>().CloseDoor();

    }


}
