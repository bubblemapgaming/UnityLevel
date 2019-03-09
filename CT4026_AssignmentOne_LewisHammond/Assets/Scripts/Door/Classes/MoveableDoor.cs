using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Class for all moveable doors, regrardless of trigger method
/// </summary>
public class MoveableDoor : MonoBehaviour
{
    #region Variable Initalistation

    //Vectors for current, target and start postion
    protected Vector3 currentPosition;
    protected Vector3 targetPosition;
    protected Vector3 startPosition;

    //Speed that the door moves at per fixed update
    [SerializeField]
    protected float doorMoveSpeed;

    //Time to wait for door to close
    [SerializeField]
    protected int doorCloseWait = 400;

    //Bool for whether we are currently moving the door
    private bool movingDoor = false;

    #endregion

    #region Properties

    public Vector3 doorPostion
    {
        get { return currentPosition; }
        set { currentPosition = doorPostion; }
    }


    #endregion  

    #region Custom Functions

    /// <summary>
    /// Function to open door to target postion and works out the transform direction (x or z axis)
    /// </summary>
    /// <param name="a_targetPostion">Target Door Postion</param>
    public void OpenDoor(Vector3 a_targetPostion)
    {
        targetPosition = a_targetPostion;
        movingDoor = true;
    }

    /// <summary>
    /// Triggers the closing of the door to its start postion
    /// </summary>
    public void CloseDoor()
    {
        targetPosition = startPosition;
        movingDoor = true;
    }

    #endregion

    //Start Function, used to initalise vars
    void Start()
    {
        //Initalse Door Postion Variables
        startPosition = transform.position;
        targetPosition = startPosition;
        currentPosition = startPosition;
    }

    /// <summary>
    /// Function that checks if the door should be moving and moves it towards target postion
    /// </summary>
    protected void MoveDoor()
    {
        if (movingDoor)
        {

            currentPosition = transform.position;

          
            //Moves the door if we are not at the current target postion
            if (currentPosition != targetPosition)
            {
                //Work out the diffrence between current and target position, 
                //if it is less than the door move amount then move by the difference
                float doorDistanceToTarget = targetPosition.y - currentPosition.y;


                //If the door is more than the move speed away from the target then move it by move speed
                //else move it by the distance that we have left
                if ((Mathf.Sign(doorDistanceToTarget) * doorDistanceToTarget) > doorMoveSpeed)
                {
                    transform.position += new Vector3(0, doorMoveSpeed * Math.Sign(doorDistanceToTarget), 0);
                }
                else
                {
                    transform.position += new Vector3(0, doorDistanceToTarget * Math.Sign(doorDistanceToTarget), 0);

                    //Move the door to exactly the target postion to protect against jitter caused by rounding errors
                    transform.position = targetPosition;
                }

            }else
            {
                movingDoor = false;
            }

        }

    }

}
