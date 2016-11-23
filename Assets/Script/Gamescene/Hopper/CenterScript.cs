using UnityEngine;
using System.Collections;



public class CenterScript : MonoBehaviour {
    public Hopperscript hopper;//father hopper
    public GameObject hopperObj;
    int testcount = 0;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider beats)
    {
        Debug.Log("Entering center"+(testcount++));
        //Time.timeScale = 0;
        hopper.enter = true;
        hopperObj.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        hopper.entering--;
        if (hopper.occupy == 0)//test if the previous beat still in the hopper
        {
            hopper.perfectvalue = 2;//now if the user touch the button it will be a perfect
        }

    }
    void OnTriggerStay(Collider beats)
    {
        if(hopper.occupy == 0)//test if the previous beat still in the hopper
        {
            Debug.Log("staying"+(testcount++));
            hopper.perfectvalue = 2;//now if the user touch the button it will be a perfect
        }
    }
    void onTriggerExit(Collider beats)
    {
        //Time.timeScale = 0;
        Debug.LogError("center exit");
        hopperObj.GetComponent<Renderer>().material.color = new Color(0, 0, 0,1);
        hopper.perfectvalue = 1;
        hopper.occupy++;//add one reamin beat in hopper

    }
}
