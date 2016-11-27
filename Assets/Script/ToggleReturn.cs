using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class ToggleReturn : MonoBehaviour, IPointerClickHandler
{

    // Use this for initialization
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Switch");
        SceneManager.LoadScene("Welcome");
        
    }
    void Start () {
        //Debug.Log("Switch");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
