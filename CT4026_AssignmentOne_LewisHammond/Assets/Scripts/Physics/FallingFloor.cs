using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour {

    [SerializeField]
    private GameObject floorSection;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //If the player has collided activate gravity and disable isKenematic,
        //If another falling floor is colliding and has gravity then activate gravity on this object
        if(other.gameObject.tag == "Player")
        {
            floorSection.GetComponent<Rigidbody>().useGravity = true;
            floorSection.GetComponent<Rigidbody>().isKinematic = false;
        }else if (other.gameObject.tag == "FallingFloor")
        {
            if(other.GetComponent<Rigidbody>().useGravity == true)
            {
                floorSection.GetComponent<Rigidbody>().useGravity = true;
                floorSection.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

    }
}
