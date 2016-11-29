using UnityEngine;
using System.Collections;
using global;
using System.IO;
using UnityEngine.SceneManagement;

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
    public int perfectnumber = 0;
    public int missnumber = 0;
    public int greatnumber = 0;
    private int maxcombo = 0;//to store the max combo
    public int health;
    public GlobalController globalC;
    public GameObject fireani;
    public GameObject healthbar;
    private static string audioname;
    private float timer;
    


    void Start () {
        Debug.Log("GameStart");
        audioname = globalC.geter();
        Debug.Log(audioname);
        loading(audioname);
        currentmusic.clip = currentclip;
        currentmusic.Play();
        //fireani.SetActive(false);
        health = 100;
        //timer = 50;
        timer = currentmusic.clip.length;
        Debug.Log("Clip length is   " + timer);
        
	    
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
        fireani.SetActive(false);
        if (currentcombo > maxcombo)
        {
            maxcombo = currentcombo;
        }
        currentcombo = 0;
    }


    // Update is called once per frame
    void FixedUpdate() {
        if (currentcombo > maxcombo)
        {
            maxcombo = currentcombo;
        }
        if (currentcombo > 0 && currentcombo % 20 == 0)
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
            //Debug.LogError("fixupdatehealth70");
        }
        else if (health < 50 && health > 30)
        {
            healthbar.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);
            //Debug.LogError("fixupdatehealth50");
        }
        else if(health < 30 && health > 10)
        {
            healthbar.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
            //Debug.LogError("fixupdatehealth30");
        }
        else if (health < 10 && health >0)
        {
            healthbar.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            //Debug.LogError("fixupdatehealth10");
        }
        else if(health <= 0)
        {
            globalC.retriveData(maxcombo, currentscore, missnumber, perfectnumber, greatnumber, false);
            Debug.Log("Health has decrease to  zore");
            SceneManager.LoadScene("Endscene");
        }
        timer = timer - Time.deltaTime;
        if (timer <= 0)
        {
            globalC.retriveData(maxcombo, currentscore, missnumber, perfectnumber, greatnumber, true);
            SceneManager.LoadScene("Endscene");
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
        if (health > 100)
        {
            health++;
        }
    }
}
