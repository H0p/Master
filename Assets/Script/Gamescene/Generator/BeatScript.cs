using UnityEngine;
using System.Collections;

public class BeatScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

<<<<<<< HEAD
    void OnTriggerEnter(Collider trig)
    {
        Destroy(gameObject);
        Debug.LogAssertion(Time.time);
=======
    void OnTriggerEnter(Collider trig)
    {
        Destroy(gameObject);
        Debug.LogAssertion(Time.time);
>>>>>>> 148f9caadb130a67894713c421fa9548e9de23eb
    }
}
