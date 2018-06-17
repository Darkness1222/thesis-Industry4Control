
using System;

namespace Industry4Control.Data
{
    [Serializable]
    internal class ParameterVector
    {
        private double[] m_MelSpectrum;
        private double m_Sum;

        public ParameterVector() { }

        public double[] GetMelSpectrum()
        {
            return m_MelSpectrum;
        }

        public void SetMelSpectrum(double[] melSpectrum)
        {
            this.m_MelSpectrum = melSpectrum;
        }

        public double GetSum()
        {
            for (int i = 0; i < m_MelSpectrum.Length; i++)
            {
                m_Sum += m_MelSpectrum[i];
            }
            return m_Sum;
        }

        public void ActivateMax(double max)
        {
            for (int i = 0; i < m_MelSpectrum.Length; i++)
            {
                m_MelSpectrum[i] = (m_MelSpectrum[i] / max) * 100;
            }
        }

        public double GetMax()
        {
            double max = 0;
            for (int i = 0; i < m_MelSpectrum.Length; i++)
            {
                if (m_MelSpectrum[i] > max)
                    max = m_MelSpectrum[i];
            }
            return max;
        }
    }
}
