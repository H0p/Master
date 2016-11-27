using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;
using parser;

namespace global
{
    public class GlobalController : MonoBehaviour
    {
        private static string S_name;
        private static bool difficulty;
        private static bool Gstauts;
        private static int maxcombo;
        private static int Gmiss;
        private static int Ggreat;
        private static int Gperfect;
        private static int Gscore;

        public static string DATAFOLDER;

        // Use this for initialization
        void Start()
        {

            //parse at the beginning of the welcome scene

            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    DATAFOLDER = Application.persistentDataPath;
                    break;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    DATAFOLDER = System.Environment.CurrentDirectory;
                    break;
                default:
                    DATAFOLDER = null;
                    break;
            }

            Debug.Log("datafolder: "+DATAFOLDER);


            //Debug.Log("controller starts");
            difficulty = false;
            Thread thread = new Thread(() => OSUb_Parser.ParseOsuFile(DATAFOLDER + @"/Osu/sample.osu"));
            thread.Start();
            




        }

        // Update is called once per frame
        void Update()
        {

            //When a song was select in selectSong scene, the bmID was changed to the BeatMapId of that song, 
            //and the trackNum should be set to the number of tracks according to difficulty

        }
        public void receive(string s)
        {
            S_name = s;
            Debug.Log("SET THE SONG: "+S_name);
        }
        public string geter()
        {
            return S_name;
        }
        public void setdiff(bool hardval)
        {
            difficulty = hardval;
        }
        public void retriveData(int combo,int score,int miss,int perfect,int great,bool status)
        {
            maxcombo = combo;
            Gscore = score;
            Gmiss = miss;
            Gperfect = perfect;
            Ggreat = great;
            Gstauts = status;
        }
        public int givercombo(){
            return maxcombo;
        }
        public int giverscore()
        {
            return Gscore;
        }
        public int givermiss()
        {
            return Gmiss;
        }
        public int giverperfect()
        {
            return Gperfect;
        }
        public int givergreat()
        {
            return Ggreat;
        }
        public bool givestatus()
        {
            return Gstauts;
        }
    }
}
