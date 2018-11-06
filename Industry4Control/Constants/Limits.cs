
namespace Industry4Control.Constants
{
    internal static class Limits
    {
        public static double VoiceAmplitudeLimit = 15;

        public static double GetLimit(double length)
        {
            if(length < 20)
            {
                return 10000;
            }
            if (length < 32)
            {
                return 20000;
            }
            if (length < 40)
            {
                return 25000;
            }
            if (length < 50)
            {
                return 30000;
            }

            return 1000000;
        }
    }
}
