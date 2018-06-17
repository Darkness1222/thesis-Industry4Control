using Industry4Control.Data;
using Industry4Control.Utils;

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
            CustomDtw customDtw = new CustomDtw(new Voice(voice));

            SavedData savedData = Helper.LoadSavedData();

            if(savedData != null)
            {
                foreach (Constants.ControlFunction key in savedData.SavedFunctions.Keys)
                {
                    if (customDtw.IsMatch(savedData.SavedFunctions[key]))
                    {
                        return new CompareResult(true,key);
                    }
                }
            }

            return new CompareResult(false);
        }
    }
}
