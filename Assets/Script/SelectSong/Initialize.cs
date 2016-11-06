using UnityEngine;
using System.Collections;
using System.IO;

public class Initialize : MonoBehaviour {
	
	public GameObject prefab;
	public GameObject[] previewButtons;  // an array of prefab instances
    // Use this for initialization
    private string filetrack;
	void Start () {

		// get the number of musci files 
		int num = getNumber();

		// instantialize prefab at running time : currently not working: modify later
		previewButtons = new GameObject[num];
		Transform tranParent = GameObject.Find ("Content").transform;
		//prefab = Resources.Load("PreviewButton") as GameObject;
		for (int i = 0; i < num; i++) {

			previewButtons[i]= Instantiate (prefab);
			previewButtons [i].transform.parent = tranParent;
		}
		/*
        filetrack = System.Environment.CurrentDirectory;
        Debug.Log(filetrack);
        DirectoryInfo current = new DirectoryInfo(filetrack);
        DirectoryInfo[] descent;
        descent = current.GetDirectories();
        for(int i = 0; i < descent.Length; i++)
        {
            Debug.Log(descent[i].Name);
        }
        */

    }

	int getNumber() {

		string[] musicfiles = Directory.GetFiles(System.Environment.CurrentDirectory + "/Assets/testmusic", "*.mp3");
		int count = musicfiles.Length;
		Debug.Log("count: " + count.ToString ());
		for (int i = 0; i < count; i++) {

			Debug.Log (musicfiles [i]);
		}

		return count;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
