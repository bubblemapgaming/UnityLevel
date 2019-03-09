using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickupObject : Interactable {

    [SerializeField]
    private float pickupRadius = 3;

    [SerializeField]
    private KeyCode pickupKey = KeyCode.E;
    [SerializeField]
    private KeyCode increaseObjHeightKey = KeyCode.T;
    [SerializeField]
    private KeyCode decreaseObjHeightKey = KeyCode.G;

    [SerializeField]
    private GameObject tooltipPrefab;

    [SerializeField]
    private float holdDistance = 3;

    [SerializeField]
    private float objectMovementSmoothing = 3;

    [SerializeField]
    private float heightChangeSpeed = 2.0f;

    private string pickupableTag = "Pickupable";

    //Vars for holding an object 
    private GameObject heldObject; //Stores the current object being held
    private GameObject selectedObject; //Stores the current object being looked at
    private bool holdingObject = false;
    private Vector3 holdPostionModification = Vector3.zero; //Holds the height mofification var for our held object

    //List to store pickupable objects
    List<GameObject> pickupableObjects;

    //Event for showing tooltip
    public delegate void ToolTipEvent(string displayText);
    public static ToolTipEvent ShowToolTip;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        #region Picking Up / Putting Down Object

        //Get the currently selected object
        selectedObject = GetSelectedObject();

        //If we have pressed the key to pick up an object, else check if an object is selected
        if (Input.GetKeyDown(pickupKey))
        {

            //If we are holding an object then drop it, else if we have an object selected then set it to our pickup object
            if (holdingObject)
            {

                //Modify Holding Object to have gravity and non-kenimatic
                if(heldObject.GetComponent<Rigidbody>() != null)
                {
                    heldObject.GetComponent<Rigidbody>().useGravity = true;
                    heldObject.GetComponent<Rigidbody>().isKinematic = false;
                }

                holdingObject = false;
                heldObject = null;

                //Reset held postion modification
                holdPostionModification = Vector3.zero;

            }else if(selectedObject != null){

                holdingObject = true;
                heldObject = selectedObject;

                //Modify Holding Object to have not gravity and be kenimatic
                if (heldObject.GetComponent<Rigidbody>() != null)
                {
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                }

            }
        }

        #endregion

        #region Tool Tips
        //Show Tool Tips
        if (ShowToolTip != null)
        {
            if (holdingObject)
            {
                ShowToolTip("Drop Object [" + pickupKey.ToString() + "]\nIncrease Object Height [" + increaseObjHeightKey.ToString() + "]\nDecrease Object Height [" + decreaseObjHeightKey.ToString() + "]");
            }
            else if (selectedObject != null)
            {
                ShowToolTip("Pickup Object [" + pickupKey.ToString() + "]");
            }
            else
            {
                ShowToolTip("");
            }
        }
        #endregion

        #region Moving Object
        if (holdingObject)
        {
            //Run Hold Object Funct
            HoldObject(heldObject, holdDistance);
        }


        #endregion

    }

    /// <summary>
    /// Returns the current object selected by the mouse
    /// </summary>
    /// <returns>Returns the currently selected object (or null if no object)</returns>
    private GameObject GetSelectedObject()
    {

        GameObject returnObject = null;

        //Get a list of possible pickupable objects
        pickupableObjects = GetPickupableObjects(pickupRadius);

        //If there are objects we can pick up
        if (pickupableObjects != null)
        {
            //Go through each pickupable object
            foreach (GameObject obj in pickupableObjects)
            {
                //See if we are looking at an object
                if (LookingAtObject(obj, Camera.main, pickupRadius))
                {
                    //If we are set the return object to that object
                    returnObject = obj;
                }
            }
        }

        //Return the object if we have one, else this will return null
        return returnObject;

    }

    /// <summary>
    /// Gets a list of pickupable objects 
    /// </summary>
    /// <param name="radius">Radius to search</param>
    /// <returns>Returnss a list of pickupable objects (or null if there are no pickupable objects)</returns>
    private List<GameObject> GetPickupableObjects(float a_radius)
    {
        //Check if there is an object in pickup range, then check if it can be picked up, then check if we are looking at it
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 10000);

        //Define list for pickupable objects
        List<GameObject> returnPickupableObjects = new List<GameObject>();

        //Check if we are in range of a collider
        if (colliders.Length > 0)
        {
            //Go through all the colliders
            foreach (Collider collider in colliders)
            {
                //If the object has "pickup-able" tag then add to the array
                if (collider.gameObject.tag == pickupableTag)
                {
                    returnPickupableObjects.Add(collider.gameObject);
                }
            }
        }

        //Return list, if the list contains items, return the list, else return null
        if (returnPickupableObjects.Count > 0)
        {
            return returnPickupableObjects;
        }
        else
        {
            return null;
        }

    }

    /// <summary>
    /// Holds object a distance from the player
    /// </summary>
    /// <param name="obj">Object to Hold</param>
    /// <param name="distance">Distance to hold object at</param>
    private void HoldObject(GameObject obj, float distance)
    {
        Vector3 objectPostion = obj.transform.position;
        Vector3 playerPostion = transform.position;


        //Check height postion increase
        if (Input.GetKey(increaseObjHeightKey))
        {
            holdPostionModification += new Vector3(0, heightChangeSpeed, 0) * Time.deltaTime;
        }else if (Input.GetKey(decreaseObjHeightKey))
        {
            holdPostionModification += new Vector3(0, -heightChangeSpeed, 0) * Time.deltaTime;
        }

        obj.transform.position = Vector3.Lerp(objectPostion, playerPostion + holdPostionModification + transform.forward * distance, Time.deltaTime * objectMovementSmoothing);
        
    }


}
