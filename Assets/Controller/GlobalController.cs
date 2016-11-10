<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;
using parser;

namespace global
{
    public class GlobalController : MonoBehaviour
    {

        



        // Use this for initialization
        void Start()
        {

            //parse at the beginning of the welcome scene

            Debug.Log("controller starts");

            Thread thread = new Thread(() => OSUb_Parser.ParseOsuFile(@".\Osu\sample.osu"));
            thread.Start();
            

        }

        // Update is called once per frame
        void Update()
        {

            //When a song was select in selectSong scene, the bmID was changed to the BeatMapId of that song, 
            //and the trackNum should be set to the number of tracks according to difficulty

        }
    }
}
=======
﻿using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;
using parser;

namespace global
{
    public class GlobalController : MonoBehaviour
    {

        



        // Use this for initialization
        void Start()
        {

            //parse at the beginning of the welcome scene

            Debug.Log("controller starts");

            Thread thread = new Thread(() => OSUb_Parser.ParseOsuFile(@".\Osu\sample.osu"));
            thread.Start();
            

        }

        // Update is called once per frame
        void Update()
        {

            //When a song was select in selectSong scene, the bmID was changed to the BeatMapId of that song, 
            //and the trackNum should be set to the number of tracks according to difficulty

        }
    }
}
>>>>>>> 148f9caadb130a67894713c421fa9548e9de23eb
