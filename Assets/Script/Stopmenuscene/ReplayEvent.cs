using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class ReplayEvent
: MonoBehaviour, IPointerClickHandler{

    // Use this for initialization
    //public GameObject resume;
    //public GameObject replay;
    //public GameObject quit;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);


        //throw new NotImplementedException();
    }

}
