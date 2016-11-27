using UnityEngine;
using System.Collections;
using global;
using UnityEngine.UI;

public class initler : MonoBehaviour {
    public GlobalController globalC;
    public Text scorenum;
    public Text combonum;
    public Text missnum;
    public Text greatnum;
    public Text perfectnum;
    public Text status;


	// Use this for initialization
	void Start () {
        scorenum.text = globalC.giverscore().ToString();
        combonum.text = globalC.givercombo().ToString();
        greatnum.text = globalC.givergreat().ToString();
        perfectnum.text = globalC.giverperfect().ToString();
        missnum.text = globalC.givermiss().ToString();
        if(globalC.givestatus()){
            status.text = "Great!";
        }
        else
        {
            status.text = "Fail!";
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
