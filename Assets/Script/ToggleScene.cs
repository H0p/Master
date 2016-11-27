using UnityEngine;
using System.Collections;
using global;
using UnityEngine.UI;

public class ToggleScene : MonoBehaviour {
    public Toggle toggle1;
    public Toggle toggle2;
    public GlobalController globalC;
	// Use this for initialization
	void Start () {
        toggle1.isOn = true;
        toggle2.isOn = false;
	}

    // Update is called once per frame
    void Update() {
        if (toggle1.isOn==true)
        {
            toggle2.isOn = false;
            globalC.setdiff(false);
        }
        if (toggle2.isOn == true)
        {
            toggle1.isOn = false;
            globalC.setdiff(true);
        }
	
	}
}
