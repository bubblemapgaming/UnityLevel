using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawnerController : Interactable {

    //Bomb Prefab
    [SerializeField]
    private GameObject bombPrefab;

    //Spawn Location Center
    [SerializeField]
    private Vector3 bombSpawnCenter;

    //Get associated text
    [SerializeField]
    private GameObject bombExplainText;

    //Seperation of bombs in X Direction
    [SerializeField]
    private float xSeperation;
    //Seperation of bombs in Z Direction
    [SerializeField]
    private float zSeperation;

    //Range to allow interacting
    [SerializeField]
    private float interactionRange = 3.0f;

    //Vars for the player object
    private float playerDistance;
    private GameObject playerObject;

    //Key Code for detonate
    [SerializeField]
    private KeyCode respawnBombsKey = KeyCode.R;

    //Event to destory all bombs created by this controller
    public delegate void BombEvent(GameObject parent);
    public static BombEvent DestroyMyBombs;
    

    //Intalise PlayerObject because awake is called after all objects are intalised
    void Awake()
    {
        playerObject = GameObject.Find("FPSController");
    }

    // Use this for initialization
    void Start () {
        //Spawn Initial Set of Bombs
        CreateAllBombs();
	}
	
	// Update is called once per frame
	void Update () {

        //If we are in range then create bombs at postions that don't already have them.
        if(PlayerInRange(interactionRange, playerObject))
        {
            //Show Help Text
            bombExplainText.SetActive(true);

            if (Input.GetKeyDown(respawnBombsKey))
            {
                //See if a bomb exists
                GameObject bombInstance = GameObject.Find("BombPickup(Clone)");

                if (bombInstance != null)
                {
                    //Destroy all bomb pick ups that are "owned" by this parent instance
                    DestroyMyBombs(gameObject);
                }

                //Spawn New Bombs
                CreateAllBombs();

                //Apply Time Penalty
                GameTimerScript.ApplyTimePenalty(10.0f);
            }
        }
        else
        {
            //Hide Help Text
            bombExplainText.SetActive(false);
        }
    }

    
    /// <summary>
    /// Creates an instance of a bomb pickup at a specified (local) location
    /// </summary>
    /// <param name="postion">Postion to spawn (relative to current object)</param>
    private void SpawnBomb(Vector3 postion)
    {
        //Create a prefab, make us the parent and then set the postion
        GameObject newBomb = Instantiate(bombPrefab);
        newBomb.transform.parent = transform;
        newBomb.transform.localPosition = postion;
    }

    /// <summary>
    /// Triggers the spawning of all bombs for this controller
    /// </summary>
    private void CreateAllBombs()
    {
        SpawnBomb(bombSpawnCenter - new Vector3(xSeperation, 0, zSeperation)); // Left Most Bomb
        SpawnBomb(bombSpawnCenter); //Center Bomb
        SpawnBomb(bombSpawnCenter + new Vector3(xSeperation, 0, zSeperation)); //Right Most Bomb
    }

}
