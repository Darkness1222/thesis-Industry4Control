using DSPLib;
using Industry4Control.Data;
using Industry4Control.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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

            double[] hannWindow = Helper.GetHannWindow(WINDOW_SIZE);
            double[] windowSizedSignal = new double[WINDOW_SIZE];
            double[] spectrum;

            ParameterVector[] pVectors = new ParameterVector[86];

            int dataCounter = WINDOW_SIZE / 2;
            int halfWindowSize = WINDOW_SIZE / 2;
            int vectorIndex = 0;

            while (dataCounter < voice.Length)
            {
                //windowing
                int from = dataCounter - halfWindowSize;
                int to = dataCounter + halfWindowSize;
                for (int i = from, j = 0; i < to; i++, j++)
                {
                    if (i < voice.Length)
                        windowSizedSignal[j] = voice[i] * hannWindow[j];
                }

                spectrum = Helper.DoFft(windowSizedSignal);

                pVectors[vectorIndex] = GetMelSpectrum(spectrum);
                vectorIndex++;
                
                dataCounter += halfWindowSize;
            }

            // DTW
            CustomDtw customDtw = new CustomDtw(pVectors);

            SavedData savedData = Helper.LoadSavedData();

            foreach(Constants.ControlFunction key in savedData.SavedFunctions.Keys)
            {
                if (customDtw.IsMatch(savedData.SavedFunctions[key]))
                {
                    return new CompareResult(true);
                }
            }



            return new CompareResult(false);
        }


        private ParameterVector GetMelSpectrum(double[] spectrum)
        {
            ParameterVector pv = new ParameterVector();
            double[] vector = new double[30];
            for (int i = 0; i < 30; i++)
            {
                for (int j = Helper.FilterLimits[i,0]; j < Helper.FilterLimits[i,1] + 1; j++)
                {
                    vector[i] += spectrum[j] * Helper.MelFilter[i,j];
                }
            }
            pv.SetMelSpectrum(vector);
            return pv;
        }

        
    }
}
