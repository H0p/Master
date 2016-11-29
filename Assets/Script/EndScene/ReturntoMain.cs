using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class ReturntoMain : MonoBehaviour, IPointerClickHandler
{

    // Use this for initialization
    void Start()
    {

    }
        public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Welcome");
        Debug.Log("Switch back to Welcome");
        //throw new NotImplementedException();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
