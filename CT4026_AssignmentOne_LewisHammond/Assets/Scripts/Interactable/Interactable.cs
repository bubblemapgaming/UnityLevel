using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    /// <summary>
    /// Checks if the player is in a certian range
    /// </summary>
    /// <param name="range">Range to check</param>
    /// <param name="playerObject">Player Object to check</param>
    /// <returns></returns>
    protected bool PlayerInRange(float range, GameObject playerObject)
    {

        //If the distance between the postions of the player is less than the range
        if (Vector3.Distance(transform.position, playerObject.transform.position) <= range)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Creates 3D Text to show a message at object postion
    /// </summary>
    /// <param name="a_textToDisplay"></param>
    /// <param name="a_prefab"></param>
    /// <param name="a_postion"></param>
    /// <param name="a_rotation"></param>
    /// <param name=""></param>
    /// <returns></returns>
    protected GameObject Create3DText(string a_textToDisplay, GameObject a_prefab, Vector3 a_postion, Vector3 a_rotation)
    {

        //Creates 3D Text Prefab to Show
        GameObject UIText = Instantiate(a_prefab);
        UIText.transform.position = transform.position + new Vector3(-0.7f, 1.2f, 0);
        UIText.transform.Rotate(a_rotation.x, a_rotation.y, a_rotation.z, Space.Self);

        //Set Text of Prefab
        UIText.GetComponent<TextMesh>().text = a_textToDisplay;

        return UIText;
    }

    protected void Modify3DText(string a_textToDisplay, GameObject a_object)
    {
        a_object.GetComponent<TextMesh>().text = a_textToDisplay;
    }



    /// <summary>
    /// Checks if we are looking at a specified object
    /// </summary>
    /// <param name="a_obj">Object to Check</param>
    /// <param name="a_range">Range to check in</param>
    /// <param name="a_sourceCamera">Camera to check from</param>
    /// <returns>Returns if we are looking at an object (or null if we are not looking at an object)</returns>
    protected bool LookingAtObject(GameObject a_obj, Camera a_sourceCamera, float a_range = Mathf.Infinity)
    {
        //Calculate the mouse postion in and offset position
        Vector3 mousePositionWithZOffset = Input.mousePosition;
        mousePositionWithZOffset.z = 10;

        //Where the camera is viewing from
        Vector3 cameraPosition = a_sourceCamera.transform.position;

        //Get the postion 10 units away from the mouse click down the camera frustrum
        Vector3 mousePostionInWorldSpace = Camera.main.ScreenToWorldPoint(mousePositionWithZOffset);

        //The Vector Diffrence fron the camera, to the click postion down frustrum
        //Gets thoe direction of the mouse clock goind down the camera frustrum
        Vector3 directionFromCameraToMouse = mousePostionInWorldSpace - cameraPosition;

        //Checks a ray cast if there is a valid place to place the prefab
        RaycastHit hitInfo;
        if (Physics.Raycast(new Ray(cameraPosition, directionFromCameraToMouse), out hitInfo, a_range))
        {
            //If the ray cast has hit the object we are looking at
            if (hitInfo.collider.gameObject == a_obj)
            {
                return true;
            }
        }

        return false;
    }

}
