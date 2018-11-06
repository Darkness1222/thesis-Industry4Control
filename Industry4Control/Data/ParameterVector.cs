
using System;

namespace Industry4Control.Data
{
    [Serializable]
    internal class ParameterVector
    {
        private short[] m_MelSpectrum;
        
        public ParameterVector() { }

        public short[] GetMelSpectrum()
        {
            return m_MelSpectrum;
        }

        public void SetMelSpectrum(short[] melSpectrum)
        {
            m_MelSpectrum = melSpectrum;
        }

        public short GetSum()
        {
            short sum = 0;
            for (int i = 0; i < m_MelSpectrum.Length; i++)
            {
                sum += m_MelSpectrum[i];
            }
            return sum;
        }

        public void ActivateMax(double max)
        {
            for (int i = 0; i < m_MelSpectrum.Length; i++)
            {
                m_MelSpectrum[i] = (short)((m_MelSpectrum[i] / max) * 100);
            }
        }

        public double GetMax()
        {
            double max = 0;
            for (int i = 0; i < m_MelSpectrum.Length; i++)
            {
                if (m_MelSpectrum[i] > max)
                {
                    max = m_MelSpectrum[i];
                }
            }
            return max;
        }
    }
}
