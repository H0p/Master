using UnityEngine;
using System.Collections;

public class Topscrpit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private Rigidbody rb;
    //private GameObject hp;
    private ConstantForce cf;
    void OnCollisionEnter(Collision collisionInfo)
    {
        // Debug.Log("Hiting something");
        if (collisionInfo.gameObject.tag.CompareTo("Hopper") == 0)
        {
			rb = collisionInfo.rigidbody;
			Vector3 vertical = new Vector3(0.0f, 0.0f, 0.0f);
			rb.velocity = vertical;
            rb.useGravity = false;
            //hp = collisionInfo.gameObject;
            cf = rb.GetComponent<ConstantForce>();
            vertical = new Vector3(0.0f, -4.5f, 0.0f);
            cf.force = vertical;

            //Debug.Log("Finish the process");
        }
    }
}
