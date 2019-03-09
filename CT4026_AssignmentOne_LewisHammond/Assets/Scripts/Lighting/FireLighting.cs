using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script Controls the lighting of light within a fire
/// </summary>
public class FireLighting : MonoBehaviour {

    [SerializeField]
    private Light fireLight;

    [SerializeField]
    private float maxLightIntensity;

    [SerializeField]
    private float minLightIntensity;

    //Bool for enabling random increases in light intensity
    [SerializeField]
    private bool enableRandomSpikes;

    //Change in light intensity per frame
    [SerializeField]
    private float lightIntesityChange;

    private float lightIntensity;

    //enum of whether the light intensity is increasing or decreasing
    private enum intensityDirections
    {
        increase, decrease
    }

    private intensityDirections intensityDirection;

    // Use this for initialization
    void Start () {
        //Start Light Intensity at minimum
        lightIntensity = minLightIntensity;
	}
	
	// Update is called once per frame
	void Update () {
		
        //Work out if we are at a boundary of max/min light
        if(lightIntensity >= maxLightIntensity)
        {
            //Set light intensity to decrease
            intensityDirection = intensityDirections.decrease;
        }
        else if(lightIntensity <= minLightIntensity)
        {
            //Set Light intensity to increase
            intensityDirection = intensityDirections.increase;
        }


        //Update Light Intensity
        //If light intensity should increase then increase it by the increase ammount and vice-versa
        if(intensityDirection == intensityDirections.increase)
        {
            lightIntensity += lightIntesityChange;
        }
        else if (intensityDirection == intensityDirections.decrease)
        {
            lightIntensity -= lightIntesityChange;
        }

        //Random Spikes
        if(Random.Range(0.0f,100.0f) < 0.2)
        {
            lightIntensity = Random.Range(minLightIntensity,maxLightIntensity);
        }

        //Apply the light intentsity change to the light itself
        fireLight.intensity = lightIntensity;
	}
}
