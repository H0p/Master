using UnityEngine;
using System.Collections;
using System.Timers;

public class selfDes : MonoBehaviour {

    public float _time;
    private float currentTime;
	// Use this for initialization
	void Start () {
        
        currentTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

        if (Time.time >= currentTime + 2.02f) {
            Destroy(this.gameObject);
        }
	}
}
