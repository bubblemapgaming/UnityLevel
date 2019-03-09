using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuControl : MonoBehaviour {

    [SerializeField]
    private GameObject landingMenu;
    [SerializeField]
    private GameObject optionsMenu;

    [SerializeField]
    private Dropdown resolutionDropdown;
    [SerializeField]
    private Toggle fullscreenToggle;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;

    //Array to store all resolutions we can have.
    private Resolution[] resolutions;

    //Stores for values that are not yet apllied
    private Resolution pendingResolution;
    private bool pendingFullscreen;
    //private float pendingMusicVolume;
    //private float pendingSFXVolume;

    // Use this for initialization
    void Start () {
        PopulateResolutionList();
	}
	
    /// <summary>
    /// Gets a list of all the resolutions supported by the current device, then adds them to the connected dropdown list
    /// </summary>
    private void PopulateResolutionList()
    {
        //Get a list of all supported resolutions
        resolutions = Screen.resolutions;

        //Clear dropdown list
        resolutionDropdown.ClearOptions();

        //String List to store strings of resolution
        List<string> resolutionStringList = new List<string>(); 

        //Format strings for each resolution, then add to array
        foreach(Resolution res in resolutions)
        {
            string resolutionString = res.width.ToString() + " x " + res.height.ToString() + " (" + res.refreshRate.ToString() + "hz)";
            resolutionStringList.Add(resolutionString);
        }

        //Add all resolution strings to drop down
        resolutionDropdown.AddOptions(resolutionStringList);

    }

    /// <summary>
    /// Gets the value selected from the resoultion box, assigns it to pending Resolution
    /// </summary>
    public void UpdatePendingResolution()
    {
        //Get the current resolution selection
        int resolutionSelectionIndex = resolutionDropdown.value;

        //Retrive what value that is from the array of resolutions
        pendingResolution = resolutions[resolutionSelectionIndex];
    }

    /// <summary>
    /// Gets the current fullscreen toggle status, 
    /// </summary>
    public void UpdatePendingFullscreen()
    {
        //Get the current toggle status
        pendingFullscreen = fullscreenToggle.isOn;
    }

   /* public void UpdatePendingMusicVolume()
    {
        //Get the current slider value
        pendingMusicVolume = musicSlider.value;
        
    }*/

    /*public void UpdatePendingSFXVolume()
    {
        //Get the current slider value
        pendingSFXVolume = sfxSlider.value;
    }*/

    /// <summary>
    /// Resets high scores
    /// </summary>
    public void ResetHighScores()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ReturnToLandingPage()
    {
        optionsMenu.SetActive(false);
        landingMenu.SetActive(true);
    }

    public void ApplyPendingOptions()
    {
        //Set the actual resolution and fullscreen status
        Screen.SetResolution(pendingResolution.width, pendingResolution.height, pendingFullscreen);

        //Apply to global gamesettings
        GameSettings.fullscreen = pendingFullscreen;
        GameSettings.resolution = pendingResolution;
        
    }
}
