using UnityEngine;
using System.Collections;

public class Healthbar : MonoBehaviour {

    // Use this for initialization
    public GameObject self;
    private Renderer render;
    private Texture texture;
	void Start () {
        render = self.GetComponent(typeof(Renderer)) as Renderer;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
