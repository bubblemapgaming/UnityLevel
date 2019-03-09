using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTriggeredDoor : MoveableDoor
{

    //UI Text Prefab to Show Infomation
    [SerializeField]
    public GameObject doorUITextPrefab;

    private int doorCloseTimer;

    // Use this for initialization
    void Start () {

        startPosition = transform.position;
        targetPosition = startPosition;
        currentPosition = startPosition;

        doorCloseTimer = 0;

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //Call Function to move door
        MoveDoor();

        //Countdown timer to close the door
        if (doorCloseTimer > 0 && currentPosition != startPosition && currentPosition == targetPosition /*Only Countdown if we are not moving the door and its not at its start position*/)
        {
            doorCloseTimer -= 1;
        }

    }

    /// <summary>
    /// Function that resets the door timer to the predefined wait close time
    /// </summary>
    public void ResetDoorTimer()
    {
        doorCloseTimer = doorCloseWait;
    }
}
