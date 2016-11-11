using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class Initialize : MonoBehaviour
{

    public GameObject prefab;
    public GameObject[] previewButtons;  // an array of prefab instances
    public AudioClip[] audioClips;
    // Use this for initialization
    // private string filetrack;
    int debug = 1;
    void Start()
    {

        // load audio files first
        loadClips();
        // get the number of musci files 
        int num = getNumber();

        // instantialize prefab at running time : currently not working: modify later
        previewButtons = new GameObject[num];
        Transform tranParent = GameObject.Find("scrolloption").transform;
        //prefab = Resources.Load("PreviewButton") as GameObject;
        for (int i = 0; i < num; i++)
        {

            // Instantiate the prefab and add it to the parent transform.
            previewButtons[i] = (GameObject)Instantiate(prefab);
            previewButtons[i].transform.SetParent(tranParent, false);

            // set up text 

            Text tm = previewButtons[i].GetComponentInChildren<Text>();
            tm.text = "preview" + i.ToString();
            Button button = previewButtons[i].GetComponentInChildren<Button>();
            button.GetComponentInChildren<Text>().text = "Song " + i.ToString();

            // add AudioSource to the button by converting it to GameObject.
            GameObject temp = Instantiate(button.gameObject);
            AudioSource audioSource = temp.AddComponent<AudioSource>();
            if (debug == 1 && audioSource == null)
            {
                Debug.Log("audioSource is null");
            }
            // assign audio clip to the corresponding audio clip

            if (debug == 1 && audioClips[i] == null)
            {

                Debug.Log("audioClips" + i.ToString() + " is null");
            }
            PlayEnter tempenter;
            tempenter = previewButtons[i].GetComponent(typeof(PlayEnter)) as PlayEnter;
            tempenter.name = audioClips[i].name;
            audioSource.clip = audioClips[i];

            if (debug == 1 && audioSource.clip == null)
            {
                Debug.Log("clip is null");
            }
            audioSource.playOnAwake = false;
            //button.onClick.AddListener(()=>playSound (audioSource)); // since we use audioButton, we do not need to add listener.


            // get AudioButton Object and assign music variable wit the corresponsing audio clip.
            AudioButton audioButton = button.GetComponentInChildren<AudioButton>();
            if (audioButton == null)
            {
                Debug.Log("audioButton is null");
            }

            audioButton.music = audioSource;
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

    int getNumber()
    {

        string[] musicfiles = Directory.GetFiles(System.Environment.CurrentDirectory + "/Assets/Resources/", "*.mp3");
        int count = musicfiles.Length;
        //Debug.Log("count: " + count.ToString ());
        for (int i = 0; i < count; i++)
        {

            //Debug.Log (musicfiles [i]);
        }

        return count;
    }


    void loadClips()
    {
        string[] musicfiles = Directory.GetFiles(System.Environment.CurrentDirectory + "/Assets/Resources/", "*.mp3");
        int num = musicfiles.Length;
        audioClips = new AudioClip[num];
        //Debug.Log("num: " + num.ToString());
        for (int i = 0; i < num; i++)
        {
            //Debug.Log("music file " + i.ToString() + musicfiles[i]);
            string name = parsePath(musicfiles[i]);
            audioClips[i] = Resources.Load(name) as AudioClip;
        }

    }
    // path formate : /Users/MyHome/Desktop/github/test/Assets/Resources/closer.mp3
    string parsePath(string path)
    {
        string removeStr = System.Environment.CurrentDirectory + "/Assets/Resources/";
        string nameExt = path.Remove(0, removeStr.Length);
        //Debug.Log("nameExt" + nameExt);
        // get rid of extention

        string name = nameExt.Remove(nameExt.Length - 4, 4);
        //Debug.Log("name" + name);
        return name;
    }


    void playSound(AudioSource so)
    {
        if (!so.isPlaying)
        {

            so.PlayOneShot(so.clip);

        }
        else {
            so.Pause();
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
