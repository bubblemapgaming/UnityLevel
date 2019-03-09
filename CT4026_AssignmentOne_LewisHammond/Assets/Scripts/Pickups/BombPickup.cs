using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : MonoBehaviour {

    private float yCenter;
    private float yBobLimit = 0.2f;
    private float yBobAmount = 0.4f; //Amount the object moves per second 

    private GameObject creator;

    private enum MoveDirection
    {
        up,
        down
    }

    private MoveDirection currentBobDirection = MoveDirection.up;

    //Subscribes/Unsubscribes for events
    private void OnEnable()
    {
        BombSpawnerController.DestroyMyBombs += CheckBombReload;
    }
    private void OnDisable()
    {
        BombSpawnerController.DestroyMyBombs -= CheckBombReload;
    }

    private void Start()
    {
        //Initalise y center to current postion
        yCenter = transform.position.y;

        //Get parent spawner object
        creator = transform.parent.gameObject;
    }

    private void Update()
    {
        //Bob the ball between the top and lower bounds (which are center +/- the bob limit)

        //Check if we are outside bounds if we are then change the direction that the object is moving
        if(transform.position.y >= (yCenter + yBobLimit))
        {
            currentBobDirection = MoveDirection.down;
        }else if(transform.position.y <= (yCenter - yBobLimit))
        {
            currentBobDirection = MoveDirection.up;
        }


        //Move the Ball
        if(currentBobDirection == MoveDirection.up)
        {
            transform.position += new Vector3(0, yBobAmount * Time.deltaTime, 0);
        }else if(currentBobDirection == MoveDirection.down)
        {
            transform.position -= new Vector3(0, yBobAmount * Time.deltaTime, 0);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //If the player has collided with the pickup
        if (other.tag == "Player")
        {

            //Increase the player number of bombs and destroy the object
            GameObject.Find("FPSController").GetComponent<CharacterConditions>().bombCount += 1;

            //Destroy the current Game Object
            Destroy(gameObject);
        }


    }

    /// <summary>
    /// Checks if the parent to check is our parent instance, if it is then destroy this bomb pickup.
    /// This Function should called when a bomb spawner controller wants to reload its bombs
    /// </summary>
    /// <param name="parentToCheck">Parent Instance that is destroying its bombs</param>
    private void CheckBombReload(GameObject parentToCheck)
    {
        //If the parent that has requested all the bombs to destory is our creator then destory this object
        if (parentToCheck == creator)
        {
            Destroy(gameObject);
        }
    }

}
