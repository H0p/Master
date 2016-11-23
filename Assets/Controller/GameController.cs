using UnityEngine;
using System.Collections;
using global;
using System.IO;

public class GameController : MonoBehaviour {

    // Use this for initialization
    public Hopperscript hopper0;
    public Hopperscript hopper1;
    public Hopperscript hopper2;
    public Hopperscript hopper3;
    public AudioSource currentmusic;
    private AudioClip currentclip;

    public  int currentscore = 0;//score for now
    public  int currentcombo = 0;//combo for now
    public int goodincre = 10;//socre to add for good
    public int perfectincre = 20;//score to add for perfect
    public int health;
    public GlobalController globalC;
    public GameObject fireani;
    public GameObject healthbar;
    private static string audioname;
    


    void Start () {
        audioname = globalC.geter();
        Debug.Log(audioname);
        loading(audioname);
        currentmusic.clip = currentclip;
        currentmusic.Play();
        fireani.SetActive(false);
        health = 100;
	    
	}
    public void addScore(int incre)//add score from each hopper to the controller
    {
        currentscore += incre;
    }
    public void addCombo()
    {
        currentcombo++;
    }
    public void clearcombo()//called when miss happened
    {
        currentcombo = 0;
    }


    // Update is called once per frame
    void FixedUpdate() {
        if (currentcombo > 0 && currentcombo % 50 == 0)
        {
            goodincre *= 2;
            perfectincre *= 2;

        }
        if (currentcombo == 0)
        {
            goodincre = 10;
            perfectincre = 20;
        }
        if (currentcombo > 20)
        {
            fireani.SetActive(true);
        }
        if (health < 70 && health > 50)
        {
            healthbar.GetComponent<Renderer>().material.color = new Color(1, 0.92f, 0.016f, 1);
            Debug.LogError("fixupdatehealth70");
        }
        else if (health < 50 && health > 30)
        {
            healthbar.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);
            Debug.LogError("fixupdatehealth50");
        }
        else if(health < 30 && health > 10)
        {
            healthbar.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
            Debug.LogError("fixupdatehealth30");
        }
        else if (health < 10)
        {
            healthbar.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            Debug.LogError("fixupdatehealth10");
        }
    }
    void loading(string name)
    {
        currentclip = Resources.Load(name) as AudioClip;
    }
    public string getcurrentname()
    {
        return audioname;
    }
    public void decrehealth()
    {
        health -= 2;
    }
    public void increhealth()
    {
        health++;
    }
}
