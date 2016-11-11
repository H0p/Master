using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class ResumeEvent : MonoBehaviour, IPointerClickHandler{

    // Use this for initialization
    public GameObject resume;
    public GameObject replay;
    public GameObject quit;
    public AudioSource currentmusic;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        resume.SetActive(false);
        replay.SetActive(false);
        quit.SetActive(false);
        Time.timeScale = 1;
        if (!currentmusic.isPlaying)
        {
            currentmusic.Play();
        }
        //Debug.Log("TestClick");
    }

}
