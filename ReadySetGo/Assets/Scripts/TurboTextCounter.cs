using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurboTextCounter : MonoBehaviour {

    //the dead battery image
    public Image deadBattery;
    //the live battery image
    public Image turboEngaged;
    

	// Use this for initialization
	void Start () {
        //player starts off with no turbo
        deadBattery.enabled = true;
        turboEngaged.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (this.gameObject.GetComponent<PlayerController>().hasTurbo)
        {
            deadBattery.enabled = false;
            turboEngaged.enabled = true;
        } else
        {
            deadBattery.enabled = true;
            turboEngaged.enabled = false;
        }
        
        //turboText.text = "Turbo Charges: " + this.gameObject.GetComponent<PlayerController>().hasTurbo.ToString();
	}
}
