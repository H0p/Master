using UnityEngine; //using some logging feature

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Linq;

namespace parser
{
    public class OSUb_Parser
    {

        const string OsuFileFolder = @".\OsuSaved\";

        public const string HeaderPattern = @"^\[([a-zA-Z0-9]+)\]$";
        public const string ValuePattern = @"^([a-zA-Z0-9]+)[ ]*:[ ]*(.+)$";

        public static List<int> CachedList;


        public static bool ParseOsuFile(string filePath)
        {

            Debug.Log("Parse File:" + filePath);
            int debug = 0;

            string name = filePath.Substring(6, filePath.Length - 10);

            Regex headeRegex = new Regex(HeaderPattern);
            var fileInfo = new OsuFileInfo();
            //var fileStream = File.Open(filePath, FileMode.Open);
            using (var fileStream = new StreamReader(filePath))
            {



                string keyWord = string.Empty;

             

                while (!fileStream.EndOfStream)
                {
                    string line = fileStream.ReadLine();
                    if (line == null) throw new Exception("string from fileStream is null");
                    if (line.StartsWith(@"//") || line.Equals(@"")) continue; //comment line, ignore
                    var match = headeRegex.Match(line);
                    //Debug.Log(" line :"+line);
                    if (match.Success)
                    {
                        keyWord = match.Groups[0].Value;
                        continue;
                    }

                    switch (keyWord)
                    {
                        case "[Editor]": //discard
                            break;
                        case "[Colours]":
                            break;
                        case "[General]":
                        case "[Metadata]":
                        case "[Difficulty]":
                            //Debug.LogError("enter: info");
                           // Debug.Log(line);
                            OsuFileInfo.ParseOsuFileInfo(fileInfo, line, keyWord);

                            break;
                        case "[Events]":
                            //Debug.LogError("enter: events");
                            OsuFileInfo.ParseOsuFileEvents(fileInfo, line);
                            break;
                        case "[TimingPoints]":
                            //Debug.LogError("enter: timing"+(debug++));
                            OsuFileInfo.ParseOsuTimingPoints(fileInfo, line);
                            break;
                        case "[HitObjects]":
                            //Debug.LogError("enter: HitO");
                            OsuFileInfo.ParseHitObject(fileInfo, line);
                            break;
                        default: //when the file start
                            break;

                    }
                }
                //Debug.Log("exit parse1");
            }
            Debug.Log("exit parse");
            //var osuFile = new OsuFile(fileInfo, filePath);
            LoadCachedList(@".\Cache\cacheList");

            if (CachedList == null) CachedList = new List<int>();
            if (!CachedList.Contains(fileInfo.BeatMapId)) CachedList.Add(fileInfo.BeatMapId);

            SaveCachedList(@".\Cache\cacheList");

            var bf = new BinaryFormatter();

            FileInfo dinfo = new FileInfo(OsuFileFolder);

            if (!dinfo.Exists)
            {

                //Debug.Log("folder");
                Directory.CreateDirectory(dinfo.Directory.FullName);
            }

            using (var fileToSave = File.Open(OsuFileFolder + name + ".osv",FileMode.Create))
            {
                bf.Serialize(fileToSave, fileInfo);
            }


            return true;
        }



        public static void LoadCachedList(string savePath)
        {
            //cachedList = new List<int>;

            XmlSerializer serializer = new XmlSerializer(typeof(List<int>));
            try
            {
                using (var stream = File.Open(savePath, FileMode.Open))
                {


                    if (stream != null)
                    {
                        OSUb_Parser.CachedList = (List<int>)serializer.Deserialize(stream);
                        stream.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
               
            }
        }

        public static void SaveCachedList(string savePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<int>));
            using (var stream = File.Open(savePath, FileMode.Create))
            {
                serializer.Serialize(stream, OSUb_Parser.CachedList);
                stream.Close();
            }
        }
    }

    [Serializable]
    public class OsuFileInfo
    {
        #region General

        public string AudioFilename = string.Empty;
        public int AudioLeadIn = -1;
        public int PreviewTime = -1;
        public string SampleSet = string.Empty;

        #endregion

        #region Difficulty Settings
        //public float DifficultyApproachRate = 5;
        //public float DifficultyCircleSize = 5;
        //public float DifficultyHpDrainRate = 5;
        public float DifficultyOverall = -1;
        //public double DifficultySliderMultiplier = 1.4;
        //public double DifficultySliderTickRate = 1;
        #endregion

        #region Metadata
        public string Artist = string.Empty;

        public string Tags = string.Empty;
        public string Title = string.Empty;
        public int BeatMapId = -1;
        #endregion

        #region Events

        public string BackGround = string.Empty;
        public string Video = string.Empty;
        public List<KeyValuePair<int, int>> BreakTimes = null;

        #endregion

        #region TimingPoints

        public List<OsuTimingPoint> TimingPoints;

        #endregion

        #region HitObjects

        public List<OsuHitObject> HitObjects;

        #endregion



        public static void ParseHitObject(OsuFileInfo info, string toParse)
        {
            var keys = toParse.Split(',');

            //BEWARE:many components of normal hitObj are discarded here for convienence
            int x = int.Parse(keys[0]);
            int time = int.Parse(keys[2]);
            int SoundType = int.Parse(keys[4]);
            OsuHitObject hitObject = new OsuHitObject(x, time, SoundType);
            //Debug.Log(hitObject.Position);
            if (info.HitObjects == null) info.HitObjects = new List<OsuHitObject>();
            info.HitObjects.Add(hitObject);
        }


        /// <summary>
        /// parse OsuFile [TimingPoints] into OsuFileInfo
        /// </summary>
        /// <param name="info">OsuFileInfo to receive parsed data</param>
        /// <param name="toParse">the line of string to parse</param>
        public static void ParseOsuTimingPoints(OsuFileInfo info, string toParse)
        {
            if (info.TimingPoints == null) info.TimingPoints = new List<OsuTimingPoint>();


            string[] keys = toParse.Split(',');

            int offset = int.Parse(keys[0]);
            float mSecPerBeat = float.Parse(keys[1]);
            int meter = int.Parse(keys[2]);
            int sampleType = int.Parse(keys[3]);
            int sampleSet = int.Parse(keys[4]);
            int volume = int.Parse(keys[5]); //0-100
            bool inherited = keys[6] == "1";//CHECK
            bool kiaiMode = keys[7] == "1";


            if (inherited)
            {
                //BEWARE: velocity is ignore
                if (mSecPerBeat <= 0)
                {
                    mSecPerBeat = info.TimingPoints.Last().MSecPerBeat;
                }
            }
            var beatPerMinute = Mathf.Round(60000 / mSecPerBeat);

            info.TimingPoints.Add(new OsuTimingPoint(offset, mSecPerBeat, beatPerMinute, meter, sampleType, sampleSet, volume, inherited, kiaiMode));


        }

        /// <summary>
        /// parse OsuFile [events] into OsuFileInfo
        /// </summary>
        /// <param name="info">OsuFileInfo to receive parsed data</param>
        /// <param name="toParse">the line of string to parse</param>
        public static void ParseOsuFileEvents(OsuFileInfo info, string toParse)
        {
            string[] keys = toParse.Split(',');
            switch (keys[0])
            {
                case "0":
                    if (keys[1] == "0")
                    {
                        if (keys[2].StartsWith("\"") && keys[2].EndsWith("\""))
                        {
                            info.BackGround = keys[2].Substring(1, keys[2].Length - 2);
                        }
                        else {
                            info.BackGround = keys[2];
                        }
                        //Debug.Log("BG Name:" + info.BackGround);

                    }
                    break;
                case "2":
                    if (!Regex.IsMatch(keys[2], @"^[0-9]+$") || !Regex.IsMatch(keys[1], @"^[0-9]+$")) goto default;
                    if (info.BreakTimes == null)
                    {
                        info.BreakTimes = new List<KeyValuePair<int, int>>();
                    }
                    info.BreakTimes.Add(new KeyValuePair<int, int>(int.Parse(keys[1]), int.Parse(keys[2])));
                    break;
                case "Video":
                    break;
                default:
                    //Debug.Log("Dropped Events type:" + keys[0]);
                    break;
            }
        }

        /// <summary>
        /// methods for parsing Osu file information, this assume a single line string to pass to this parser
        /// </summary>
        /// <param name="info">OsuFileInfo to receive parsed data</param>
        /// <param name="toParse">the line of string to parse</param>
        /// <param name="type">type of info to parse</param>
        public static void ParseOsuFileInfo(OsuFileInfo info, string toParse, string type)
        {


            //Debug.Log(toParse);
            var match = Regex.Match(toParse, OSUb_Parser.ValuePattern);
            var attriName = match.Groups[1].Value;
            var attriValue = match.Groups[2].Value;


            //Debug.Log("FileInfo: "+attriName + " " + attriValue);

            switch (type)
            {
                case "[General]":
                    switch (attriName)
                    {
                        case "AudioFilename":
                            info.AudioFilename = attriValue;
                            break;
                        case "AudioLeadIn":
                            info.AudioLeadIn = int.Parse(attriValue);
                            break;
                        case "PreviewTime":
                            info.PreviewTime = int.Parse(attriValue);
                            break;
                        case "SampleSet":
                            info.SampleSet = attriValue;
                            break;
                        default:
                            break;
                    }

                    break;
                case "[Difficulty]":
                    switch (attriName)
                    {
                        case "DifficultyOverall":
                            info.DifficultyOverall = int.Parse(attriValue);
                            break;
                        default:
                            break;
                    }

                    break;
                case "[Metadata]":
                    switch (attriName)
                    {
                        case "Artist":
                            info.Artist = attriValue;
                            break;
                        case "Tags":
                            info.Tags = attriValue;
                            break;
                        case "Title":
                            info.Title = attriValue;
                            break;
                        case "BeatmapID":
                            info.BeatMapId = int.Parse(attriValue);
                            //Debug.LogError("beatmap: "+info.BeatMapId);
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    Debug.LogError("this type should never be parse" + type);
                    break;
            }

        }

    }

    /*
    [Serializable]
    public class OsuFile
    { //CHECK
        public readonly string FilePath;
        public readonly OsuFileInfo Info;

        [field: NonSerialized]
        private bool _isLoaded = false;
        public bool IsLoaded
        {
            get { return _isLoaded; }
            private set { _isLoaded = value; }
        }

        [field: NonSerialized]
        private List<MHitObject> _hitObjects;

        public OsuFile(string filePath)
        {
            FilePath = filePath;
            this.Info = new OsuFileInfo();
            //this._hitObjects = new List<MHitObject>();
        }

        public OsuFile(OsuFileInfo info, string filePath)
        {
            this.Info = info;
            FilePath = filePath;

        }

        public void LoadHitObject(List<MHitObject> hitObjects)
        {
            if (IsLoaded) return;

            this._hitObjects = hitObjects;
            this.IsLoaded = true;
        }

    }
    */

    [Serializable]
    public struct OsuTimingPoint
    {
        public OsuTimingPoint(int offset, float mSecPerBeat, float beatPerMinute, int meter, int sampleType, int sampleSet, int volume, bool inherited, bool kiaiMode)
        {
            Offset = offset;
            MSecPerBeat = mSecPerBeat;
            BeatPerMinute = beatPerMinute;
            Meter = meter;
            SampleType = sampleType;
            SampleSet = sampleSet;
            Volume = volume;
            Inherited = inherited;
            KiaiMode = kiaiMode;
        }

        public readonly int Offset;
        public readonly float MSecPerBeat;
        public readonly float BeatPerMinute;
        public readonly int Meter;
        public readonly int SampleType;
        public readonly int SampleSet;
        public readonly int Volume; //0-100
        public readonly bool Inherited;
        public readonly bool KiaiMode;
    }


    [Serializable]
    public struct OsuHitObject
    {
        public OsuHitObject(int x, int time, int soundType)
        {

            X = x;
            Time = time;
            SoundType = soundType;
        }

        
        public readonly int X;

        public readonly int Time;

        public readonly int SoundType;
    }

}
