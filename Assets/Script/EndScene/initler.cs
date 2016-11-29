using UnityEngine;
using System.Collections;
using global;
using UnityEngine.UI;

public class initler : MonoBehaviour {
    public GlobalController globalC;
    public Button scorenum;
    public Button combonum;
    public Button missnum;
    public Button greatnum;
    public Button perfectnum;
    public Button status;


	// Use this for initialization
	void Start () {
        scorenum.GetComponentInChildren<Text>().text = globalC.giverscore().ToString();
        combonum.GetComponentInChildren<Text>().text = globalC.givercombo().ToString();
        greatnum.GetComponentInChildren<Text>().text = globalC.givergreat().ToString();
        perfectnum.GetComponentInChildren<Text>().text = globalC.giverperfect().ToString();
        missnum.GetComponentInChildren<Text>().text = globalC.givermiss().ToString();
        Debug.Log("Game data:   " + globalC.givermiss());
        if(globalC.givestatus()){
            status.GetComponentInChildren<Text>().text = "Great!";
            Debug.Log("True");
        }
        else
        {
            status.GetComponentInChildren<Text>().text = "Fail!";
            Debug.Log("False");
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
