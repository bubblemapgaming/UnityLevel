using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour {

    private GameObject playerObject;

    private string totalGameTime;
    private string totalPenaltyTime;

    //Intalise PlayerObject because awake is called after all objects are intalised
    void Awake()
    {
        playerObject = GameObject.Find("FPSController");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //We have entered the trigger to end the game
            //Stop the player being in escape mode
            playerObject.GetComponent<CharacterConditions>().isEscaping = false;

            //Go to Game End Scene
            SceneManager.LoadScene("EndGameScene");
        }

    }


}
