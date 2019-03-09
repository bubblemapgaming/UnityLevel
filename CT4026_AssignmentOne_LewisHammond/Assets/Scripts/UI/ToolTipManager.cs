using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipManager : MonoBehaviour {

    [SerializeField]
    private Text textFeild;

    //Subscribe/Unsubscribes for Events
    void OnEnable()
    {
        PickupObject.ShowToolTip += DisplayText;
    }
    private void OnDisable()
    {
        PickupObject.ShowToolTip -= DisplayText;
    }

    // Use this for initialization
    void Start () {
        textFeild.text = string.Empty;
	}
	
    void DisplayText(string text)
    {
        textFeild.text = text;
    }

}
