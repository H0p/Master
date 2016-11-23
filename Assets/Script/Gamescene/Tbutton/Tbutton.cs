using UnityEngine;
using System.Collections;


public class Tbutton : MonoBehaviour {
    public Hopperscript hopper;
    public GameController Gamecontroller;
	private GameObject ObjectD;//Waiting for destory

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void touched()
    {
        if (hopper.ifperfect() == 1)//user touch the button when the beats only heats the Hopper
        {
            if (hopper.occupy != 0)
            {
                hopper.occupy--;
            }
            Gamecontroller.increhealth();
            hopper.addGScore();//add good score
            Gamecontroller.addCombo();//add one combo
            hopper.currentbeats = hopper.beatsqueue.Dequeue(); //dequeue the beats which has already been played
			ObjectD = hopper.currentbeats.gameObject;
            hopper.currentbeats.gameObject.SetActive(false);//set the beat inactive
			Destroy(ObjectD);//destory obj
            
         
        }
        if(hopper.ifperfect() == 2)//user touch the button when the beats heats the center
        {
            Gamecontroller.increhealth();
            hopper.addPScore();//add perfect score
            Gamecontroller.addCombo();//add one combo
            hopper.currentbeats = hopper.beatsqueue.Dequeue(); //dequeue the beats which has already been played
            ObjectD = hopper.currentbeats.gameObject;//assigned the object for destory
			hopper.currentbeats.gameObject.SetActive(false);//set the beat inactive
			Destroy(ObjectD);//destore object
        }


    }
}
