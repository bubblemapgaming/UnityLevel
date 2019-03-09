using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlacement : MonoBehaviour {

    //Bomb Prefab to spawn
    [SerializeField]
    private GameObject bombPrefab;

    //Gameobject for storing the instance that is spawned to preview the placement area
    private GameObject previewInstance;

    //Array of all bombs
    private List<GameObject> bombList = new List<GameObject>();

    [SerializeField]
    private Color bombColour;

    [SerializeField]
    private float targetingRange = Mathf.Infinity;

    //Bomb Detonation Event
    public delegate void BombEvent();
    public static BombEvent DetonateBombs;

    //Key for detonate bombs
    [SerializeField]
    private KeyCode detonateBombsKey = KeyCode.F;

    //Enum for placement type of the bomb prefab (preview or place)
    private enum placementType
    {
        Place,
        Preview
    }

    // Update is called once per frame
    void Update () {

        #region Bomb Placement

        //Destroy the old bomb preview instance, if one exists
        if (previewInstance != null)
        {
            Destroy(previewInstance);
        }

        //If the Player has bombs
        if (gameObject.GetComponent<CharacterConditions>().bombCount > 0)
        {
            //Calculate the mouse postion in and offset position
            Vector3 mousePositionWithZOffset = Input.mousePosition;
            mousePositionWithZOffset.z = 10;

            //Where the camera is viewing from
            Vector3 cameraPosition = Camera.main.transform.position;

            //Get the postion 10 units away from the mouse click down the camera frustrum
            Vector3 mousePostionInWorldSpace = Camera.main.ScreenToWorldPoint(mousePositionWithZOffset);

            //The Vector Diffrence fron the camera, to the click postion down frustrum
            //Gets thoe direction of the mouse clock goind down the camera frustrum
            Vector3 directionFromCameraToMouse = mousePostionInWorldSpace - cameraPosition;

            //Checks a ray cast if there is a valid place to place the prefab
            RaycastHit hitInfo;
            if (Physics.Raycast(new Ray(cameraPosition, directionFromCameraToMouse), out hitInfo, targetingRange))
            {

                //Create an instance of the prefab to preview the postion
                previewInstance = CreateBombPrefab(hitInfo, bombPrefab, placementType.Preview);

                //Set preview instances placement type
                previewInstance.GetComponent<BombScript>().placedType = BombScript.placeType.preview;

                //If left click is pressed place an instace
                if (Input.GetMouseButtonDown(0)) {
                    //Place a bomb instance
                    GameObject placedInstance = CreateBombPrefab(hitInfo, bombPrefab, placementType.Place);

                    //Set placed bomb placement mode to placed
                    placedInstance.GetComponent<BombScript>().placedType = BombScript.placeType.placed;

                    //Add placed bomb to bomb list
                    bombList.Add(placedInstance);

                    //Decrease Player Bomb Number
                    gameObject.GetComponent<CharacterConditions>().bombCount -= 1;
                }

            }
        }

        #endregion


        #region Bomb Detonation

        //If the 'F' Key has been pressed then trigger the bomb detonation event
        if (Input.GetKeyDown(detonateBombsKey))
        {
            //Check that we have bombs to detonate
            if (bombList.Count != 0)
            {
                DetonateBombs();
                bombList.Clear();
            }
        }

        #endregion

    }

    /// <summary>
    /// Creates an instance of a bomb, given the hit info of a raycast, the prefab and the placement mode (this controls the alpha of the instance).
    /// </summary>
    /// <param name="hitInfo">Raycast to check placement postion against</param>
    /// <param name="prefab">Prefab to place</param>
    /// <param name="placement">Placement mode (preview or place)</param>
    /// <returns></returns>
    private GameObject CreateBombPrefab(RaycastHit hitInfo, GameObject prefab, placementType placement)
    {
        //Now Create a bomb
        GameObject prefabInstance = Instantiate(prefab);
        //Parent it to what we hit
        prefabInstance.transform.parent = hitInfo.collider.transform;

        //Set its postion
        prefabInstance.transform.position = hitInfo.point;

        //Set Rotation to Zero, identity is 0,0,0
        prefabInstance.transform.localRotation = Quaternion.identity;

        //Set Scale to one
        prefabInstance.transform.localScale = Vector3.one;

        //Get the estimated truer scale (based on parents)
        //E.G If parents scale is 2, out local scale is one. Our true scale is 2
        Vector3 trueScale = prefabInstance.transform.lossyScale;

        //Set localscale to 1/truescale. Compensate for the parent scale
        prefabInstance.transform.localScale = new Vector3(1 / trueScale.x, 1 / trueScale.y, 1 / trueScale.z);

        //Set Alpha, lower alpha for preview instances. Full Alpha for placed instances
        if (placement == placementType.Preview)
        {
            bombColour.a = 0.02f;
            prefabInstance.GetComponent<Renderer>().material.color = bombColour;
        }
        else
        {
            bombColour.a = 1.0f;
            prefabInstance.GetComponent<Renderer>().material.color = bombColour;
        }

        return prefabInstance;

    }

}