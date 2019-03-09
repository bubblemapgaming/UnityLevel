using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    [SerializeField]
    private Text invText;

    GameObject playerObject;

	// Use this for initialization
	void Start () {
        playerObject = GameObject.Find("FPSController");

        invText.text = string.Empty;
	}
	
	// Update is called once per frame
	void Update () {

        //Get the number of bombs the player has 
        int playerBombCount = playerObject.GetComponent<CharacterConditions>().bombCount;

        if(playerBombCount > 0)
        {
            invText.text = "<b>Inventory</b>\n<i>Bombs (" + playerBombCount + ")</i>";
        }
        else
        {
            invText.text = string.Empty;
        }

	}
}
