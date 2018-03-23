
namespace Industry4Control.Data
{
    internal class ParameterVector
    {
        private double[] melSpectrum;
        private double averageValue;

        public ParameterVector() { }

        public double[] GetMelSpectrum()
        {
            return melSpectrum;
        }

        public void SetMelSpectrum(double[] melSpectrum)
        {
            this.melSpectrum = melSpectrum;
        }

        public double GetAverageValue()
        {
            for (int i = 0; i < melSpectrum.Length; i++)
            {
                averageValue += melSpectrum[i];
            }
            return averageValue / melSpectrum.Length;
        }

        public void ActivateMax(double max)
        {
            for (int i = 0; i < melSpectrum.Length; i++)
            {
                melSpectrum[i] = (melSpectrum[i] / max) * 100;
            }
        }

        private double GetMax()
        {
            double max = 0;
            for (int i = 0; i < melSpectrum.Length; i++)
            {
                if (melSpectrum[i] > max)
                    max = melSpectrum[i];
            }
            return max;
        }

        public void SetAverageValue(double averageValue)
        {
            this.averageValue = averageValue;
        }
    }
}
