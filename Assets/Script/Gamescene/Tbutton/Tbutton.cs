using UnityEngine;
using System.Collections;


public class Tbutton : MonoBehaviour {
    public Hopperscript hopper;
    public GameController Gamecontroller;
    public GameObject hopperobj;
	private GameObject ObjectD;//Waiting for destory
    public GameObject thisbutton;
    private bool touchstatus;

    // Use this for initialization
    void Start () {
        //zthisbutton = this.GetComponent<GameObject>();
        touchstatus = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (touchstatus)
        {
            Debug.Log("Touch comfirmed!");
            Debug.Log("Perfectval is  " + hopper.ifperfect());
            if (hopper.ifperfect() == 1)//user touch the button when the beats only heats the Hopper
            {
                Debug.Log("Entering great");
                if (hopper.occupy != 0)
                {
                    hopper.occupy--;
                }
                Gamecontroller.greatnumber++;
                Gamecontroller.increhealth();
                hopper.addGScore();//add good score
                Gamecontroller.addCombo();//add one combo
                hopper.currentbeats = hopper.beatsqueue.Dequeue(); //dequeue the beats which has already been played
                ObjectD = hopper.currentbeats.gameObject;
                hopper.currentbeats.gameObject.SetActive(false);//set the beat inactive
                
                Destroy(ObjectD);//destory obj


            }
            else if (hopper.ifperfect() == 2)//user touch the button when the beats heats the center
            {
                hopperobj.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);
                Debug.Log("Entering perfect");
                Gamecontroller.perfectnumber++;
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
    void Touched()
    {
        touchstatus = true;
        thisbutton.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        Debug.Log("Button touchend!!!");
    }
    void leave()
    {
        touchstatus = false;
        thisbutton.GetComponent<Renderer>().material.color = new Color(0, 0, 0,1);
        Debug.Log("Button Leaved!!!");
    }
}
