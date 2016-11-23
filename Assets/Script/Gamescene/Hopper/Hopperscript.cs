using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class Hopperscript : MonoBehaviour {
    private int scoreforround = 0;//score for one round;
    public int perfectvalue = 0;//for button to determine if the hopper is triggering beats(good or perfect)
    public GameController gamecontroller;//the controller
    public Collider currentbeats;//the current beats which need to be setactive false
    public Queue <Collider>beatsqueue = new Queue<Collider>();//queue for the beats entering the hopper
    public Queue<int[,]> tester = new Queue<int[,]>();//queue to see if is the same beats entering second times.
    public int occupy = 0;//the number of current beat leaving the center but still in the hopper
    public int entering = 0;//the number of current beat entering hopper but not yet entering center
	private GameObject ObjectD;//Waiting for destory
    public GameObject self;
    Rigidbody currentRigibody;
    int testcount = 0;
    int _testcount = 0;
    public bool enter = false;

    void start()
    {
        currentRigibody = self.GetComponent(typeof(Rigidbody)) as Rigidbody;
        currentRigibody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
    void update(){

           
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        //Debug.Log("Hiting something");
        if (collisionInfo.gameObject.tag.CompareTo("Beatspad") == 0)//push the score to controller when hopper hit the pad
        {
	            gamecontroller.addScore(scoreforround);//add the current score in the hopper into controller to refreash the score board
			scoreforround=0;
        }
        
    }
    void  OnTriggerEnter(Collider beats)
    {
        int[,] array = new int[1,2];
        array[0,0] = 0;
        array[0,1] = 0;
        tester.Enqueue(array);
        Debug.Log("Hopper enter"+(_testcount++));
        perfectvalue = 1;//if user touch the button now will get a good
        if (enter == false) beatsqueue.Enqueue(beats);
        entering++;
    }

    void OnTriggerExit(Collider beats)
    {
        GameObject ObjectD = new GameObject();
        if (enter == true)
        {
            Debug.Log("Hopper exit7" + (testcount++));
            self.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);
            perfectvalue = 1;
            occupy++;//add one reamin beat in hopper
            enter = false;
        }
        else
        {
            Debug.Log("Hopper exit8" + (testcount++));
            currentbeats = beatsqueue.Dequeue();//deqeue the exiting beat
            ObjectD = currentbeats.gameObject;
            ObjectD.SetActive(false);
            Destroy(ObjectD);//destory obj
            occupy--;//minus one beats beacuase of leaving
            gamecontroller.decrehealth();
            if (entering == 0 && occupy == 0 && perfectvalue != 2)
            {
                perfectvalue = 0;//reset the perfect value
            }
            gamecontroller.clearcombo();//clear the combo in controller
        }
        /*Time.timeScale = 0;
        //
        //Time.timeScale = 0;
		occupy--;//minus one beats beacuase of leaving
        gamecontroller.decrehealth();
		if(entering == 0 && occupy ==0 &&perfectvalue != 2)
        {
            perfectvalue = 0;//reset the perfect value
        }
        gamecontroller.clearcombo();//clear the combo in controller
        currentbeats = beatsqueue.Dequeue();//deqeue the exiting beat
		ObjectD =currentbeats.gameObject;
        
        //currentbeats.gameObject.SetActive(false);// mark one miss, destory the beat
        */
    }
    public int ifperfect()
    {
        return perfectvalue; //return the perfectvalue
    }
    public void addGScore()
    {
        scoreforround += gamecontroller.goodincre;//add good score into the score of this round    
    }
    public void addPScore()
    {
        scoreforround += gamecontroller.perfectincre;//add perfect score into the score of this round
    }
    public void setKinematic(bool val)
    {
        if(val == true)
        {
            currentRigibody.isKinematic = true;
        }
        else
        {
            currentRigibody.isKinematic = false;
        }
        
    }
    
}
