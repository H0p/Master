using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using global;

public class PlayEnter : MonoBehaviour,IPointerClickHandler {
    private string name;
    public GlobalController GlobalC;
    public void OnPointerClick(PointerEventData eventData)
    {
        GlobalC.receive(name);
        SceneManager.LoadScene("Game-1");
        Debug.Log("Playing now");
        //throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
