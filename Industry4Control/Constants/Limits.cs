
namespace Industry4Control.Constants
{
    internal static class Limits
    {
        public static double VoiceAmplitudeLimit = 20;

        public static double GetLimit(double length)
        {
            if(length < 20)
            {
                return 200;
            }
            if (length < 30)
            {
                return 500;
            }
            if (length < 40)
            {
                return 800;
            }
            if (length < 50)
            {
                return 1200;
            }
            if (length < 60)
            {
                return 1800;
            }
            if (length < 70)
            {
                return 2500;
            }

            return 10000;
        }
    }
}
