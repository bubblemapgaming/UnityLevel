using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeLight : MonoBehaviour {

    //Subscribes/Unsubscribes for light toggle event
    void OnEnable()
    {
        EscapeLightController.toggleLights += toggleLight;
    }

    void OnDisable()
    {
        EscapeLightController.toggleLights -= toggleLight;
    }

    // Use this for initialization
    void Start () {
        //Set own Colour and disabled state
        gameObject.GetComponent<Light>().enabled = false;
        gameObject.GetComponent<Light>().color = Color.red;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Toggles the enabled status of the light
    /// </summary>
    private void toggleLight()
    {
        gameObject.GetComponent<Light>().enabled = !gameObject.GetComponent<Light>().enabled;
    }
}

