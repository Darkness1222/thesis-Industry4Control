using Industry4Control.Constants;
using Industry4Control.Data;
using Industry4Control.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Industry4Control.BusinessLogic
{
    internal class VoiceChecker
    {
        private const int WINDOW_SIZE = 512;

        public CompareResult Compare(short[] voice)
        {
            /*
             *  1. FFT
             *  2. Mel scale
             *  3. "small" dtw
             *  4. "big" dtw
             * */

            // DTW
            var watch = System.Diagnostics.Stopwatch.StartNew();

            CustomDtw customDtw = new CustomDtw(new Voice(voice));

            SavedData savedData = Helper.LoadSavedData();

            if (savedData != null)
            {
                IList<Tuple<double, int, ControlFunction>> results = new List<Tuple<double, int, ControlFunction>>();
                foreach (ControlFunction key in savedData.SavedFunctions.Keys)
                {
                    Tuple<double, int> result = customDtw.Compare(savedData.SavedFunctions[key]);

                    if (result == null) continue;

                    results.Add(new Tuple<double, int, ControlFunction>(result.Item1, result.Item2, key));
                }

                if (results.Count > 0)
                {
                    double minValue = results.Min(x => x.Item1);
                    Tuple<double, int, ControlFunction> minResult = results.First(x => x.Item1 == minValue);

                    if (minValue <= 10)
                    {
                        return new CompareResult(true, minResult.Item3);
                    }

                    int minStepsValue = results.Min(x => x.Item2);
                    Tuple<double, int, ControlFunction> minSteps = results.First(x => x.Item2 == minStepsValue);

                    if (minStepsValue <= customDtw.Voice.ParameterVectors.Length * 1.2)
                    {
                        if (minSteps == minResult || minValue < Limits.GetLimit(customDtw.Voice.ParameterVectors.Length))
                        {
                            return new CompareResult(true, minResult.Item3);
                        }
                    }
                    
                }

            }

            watch.Stop();
            Console.WriteLine("Total of total : " + watch.ElapsedMilliseconds);

            return new CompareResult(false);
        }
    }
}
