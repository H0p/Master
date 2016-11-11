using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;
public class Quitscirpt : MonoBehaviour,IPointerClickHandler{
    public AudioSource currentmusic;
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Welcome");
        //Time.timeScale = 0;
        if (currentmusic.isPlaying)
        {
            currentmusic.Pause();
        }
        Debug.Log("Quiting the game");
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
