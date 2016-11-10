using UnityEngine;
using System.Collections;

public class BeatScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider trig)
    {
        Destroy(gameObject);
        Debug.LogAssertion(Time.time);
    }
}
