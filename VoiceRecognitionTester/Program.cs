using Industry4Control.BusinessLogic;
using Industry4Control.Constants;
using Industry4Control.Data;
using Industry4Control.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VoiceRecognitionTester
{
    class Program
    {
        static void Main(string[] args)
        {
            int saveNumber = 0;
            IList<short> streamData = new List<short>();
            
            if(args.Length == 0)
            {
                return;
            }

            if(args.Length == 2)
            {
                saveNumber = int.Parse(args[1]);
            }

            using (StreamReader reader = new StreamReader(args[0]))
            {
                while (!reader.EndOfStream)
                {
                    streamData.Add(short.Parse(reader.ReadLine()));
                }
            }

            if(saveNumber > 0)
            {
                // save
                Console.WriteLine("Saving");
                Helper.SaveControlVoice(streamData.ToArray(), (ControlFunction)saveNumber);
            }
            else
            {
                Console.WriteLine("Checking...");
                // check
                CustomDtw customDtw = new CustomDtw(new Voice(streamData.ToArray()));

                SavedData savedData = Helper.LoadSavedData();

                foreach(ControlFunction key in savedData.SavedFunctions.Keys)
                {
                    double result = customDtw.Compare(savedData.SavedFunctions[key]).Item1;
                    Console.WriteLine(result);
                }

                Console.WriteLine("Finished");
                Console.ReadKey();
            }
        }
    }
}
