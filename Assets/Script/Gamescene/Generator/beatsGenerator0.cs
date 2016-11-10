using UnityEngine;
using System.Collections.Generic;
using parser;
using System.Runtime.Serialization.Formatters.Binary;
using System.Timers;
using global;
using System.Threading;
using System.IO;

public class beatsGenerator0 : MonoBehaviour {

    

    public struct beatOnTrack
    {
        public beatOnTrack(float time, int sound)
        {
            Time = time;
            Sound = sound;
        }
        public readonly float Time;
        public readonly int Sound;
    }

    public static List<beatOnTrack> track_1;
    public static List<beatOnTrack> track_2;
    public static List<beatOnTrack> track_3;
    public static List<beatOnTrack> track_4;

    public static int bmID
    {
        get;
        private set;
    }
    public static int trackNum
    {
        get;
        private set;
    }

    public void setSongInfo(int id, int trackID)
    {
        bmID = id;
        trackNum = trackID;

        if (bmID != 0 && trackNum != 0)
        {

            string path = string.Format(@".\OsuSaved\" + bmID + ".osv");
            var bf = new BinaryFormatter();
            var stream = File.Open(path, FileMode.Open);
            var fileinfo = new OsuFileInfo();
            fileinfo = (OsuFileInfo)bf.Deserialize(stream);
            stream.Close();

            //medium difficulty
            if (trackNum == 3)
            {
                int size = fileinfo.HitObjects.Count;

                for (int i = 0; i < size; i++)
                {
                    OsuHitObject osuhit = fileinfo.HitObjects[i];
                    int track = (int)osuhit.X / (int)(512 / trackNum);
                    if (track == 0)
                    {
                        if (track_1 == null) track_1 = new List<beatOnTrack>();
                        track_1.Add(new beatOnTrack(osuhit.Time, osuhit.SoundType));
                    }
                    /*else if (track == 1)
                    {
                        if (track_2 == null) track_2 = new List<beatOnTrack>();
                        track_2.Add(new beatOnTrack(osuhit.Time, osuhit.SoundType));
                    }
                    else
                    {
                        if (track_3 == null) track_3 = new List<beatOnTrack>();
                        track_3.Add(new beatOnTrack(osuhit.Time, osuhit.SoundType));
                    }*/
                }
            }

            //hard difficulty
            else if (trackNum == 4)
            {
                int size = fileinfo.HitObjects.Count;

                for (int i = 0; i < size; i++)
                {
                    OsuHitObject osuhit = fileinfo.HitObjects[i];
                    int track = (int)osuhit.X / (int)(512 / trackNum);
                    if (track == 0)
                    {
                        if (track_1 == null) track_1 = new List<beatOnTrack>();
                        track_1.Add(new beatOnTrack(osuhit.Time, osuhit.SoundType));
                        Debug.Log("track_1: " + osuhit.Time + ", " + osuhit.SoundType);
                    }
                    /*else if (track == 1)
                    {
                        if (track_2 == null) track_2 = new List<beatOnTrack>();
                        track_2.Add(new beatOnTrack(osuhit.Time, osuhit.SoundType));
                        Debug.Log("track_2: " + osuhit.Time + ", " + osuhit.SoundType);
                    }
                    else if (track == 2)
                    {
                        if (track_3 == null) track_3 = new List<beatOnTrack>();
                        track_3.Add(new beatOnTrack(osuhit.Time, osuhit.SoundType));
                        Debug.Log("track_3: " + osuhit.Time + ", " + osuhit.SoundType);
                    }
                    else
                    {
                        if (track_4 == null) track_4 = new List<beatOnTrack>();
                        track_4.Add(new beatOnTrack(osuhit.Time, osuhit.SoundType));
                        Debug.Log("track_4: " + osuhit.Time + ", " + osuhit.SoundType);
                    }*/
                }
            }
        }

        bmID = 0;
        trackNum = 0;
    }












    


    int count;
    public int built;
    int updown;
    int size1;
    float nextround;
    float falltime;
    int buildCount;

    public GameObject beat;

    // Use this for initialization
    void Start () {

        setSongInfo(374115, 4);
        size1 = track_1.Count;
        count = 0;
        built = 0;
        updown = 1;
        falltime = 2.02f;
        nextround = 0;
        buildCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= nextround)
        {
            
            nextround += falltime;

            buildCount = 0;

            while (count <= size1)
            {
                
                while (track_1[count].Time < nextround*1000)
                {
                    built = 1;
                    float timeConsumed = Time.time;
                    //Dynamic Generate
                    //None, normal, whistle, finish, clap: 0 1 2 4 8
                    float soundtype = track_1[count].Sound / 1000;

                    float measure = 0.5f * 4.5f *
                            ((float)(track_1[count].Time / 1000.000) - timeConsumed) *
                                ((float)(track_1[count].Time / 1000.000) - timeConsumed);
                    
                    float y;
                    if (updown == 1)
                    {
                        y = 4.5f - measure;
                    }
                    else y = -4.5f + measure;

                    beat.GetComponent<Transform>().position = new Vector3(-1.875f, y, -0.75f);
                    beat.GetComponent<Transform>().rotation = new Quaternion(0.0f, soundtype, 0.0f, 0.0f);
                    if (measure >= 1)
                    {
                        Instantiate(beat);
                        buildCount++;
                        //Debug.LogError("finish building: " + Time.time + " with updown " + updown+" at y = "+y);
                        //Debug.LogError("measure(" + measure + ") = 0.5 * 4.5 * [beatTime(" + (float)(track_1[count].Time / 1000) +
                            //")-timeConsumed(" + timeConsumed + ")]^2");
                    }
                    count++;
                }

                if (true) updown = 1 - updown; //CHECK
                built = 0;
                break;
            }
        }
    }
}
