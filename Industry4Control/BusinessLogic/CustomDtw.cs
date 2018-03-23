using Industry4Control.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industry4Control.BusinessLogic
{
    internal class CustomDtw
    {
        private const double DTWLIMIT = 900.0;
        private ParameterVector[] m_ParameterVector;

        public CustomDtw(ParameterVector[] parameterVectors)
        {
            m_ParameterVector = parameterVectors;
        }

        public bool IsMatch(ParameterVector[] savedParameterVector)
        {
            // Todo: write it

            double[,] bigDtwMatrix = new double[savedParameterVector.Length, savedParameterVector.Length];

            for(int i=0;i< savedParameterVector.Length; i++)
            {
                for(int j = 0; j < m_ParameterVector.Length; j++)
                {
                    bigDtwMatrix[i, j] = RunSmallDtw(m_ParameterVector[i], savedParameterVector[j]);
                }
            }

            double result = RunBigDTW(bigDtwMatrix);

            if(result < DTWLIMIT)
            {
                return true;
            }

            return false;
        }

        private double RunSmallDtw(ParameterVector vector1, ParameterVector vector2)
        {
            double[] melSpectrum1 = vector1.GetMelSpectrum();
            double[] melSpectrum2 = vector2.GetMelSpectrum();

            double[,] dtwMatrix = new double[30,30];

            for(int i = 0; i < 30; i++)
            {
                dtwMatrix[i, 0] = double.MaxValue;
                dtwMatrix[0, i] = double.MaxValue;
            }
            dtwMatrix[0, 0] = 0;

            double cost;
            for(int i = 0; i < 30; i++)
            {
                for(int j = 0; j < 30; j++)
                {
                    cost = Math.Abs(melSpectrum1[i] - melSpectrum2[j]);
                    dtwMatrix[i, j] = cost + Minimum(dtwMatrix[i-1,j], dtwMatrix[i, j-1], dtwMatrix[i-1, j-1]);
                }
            }

            return dtwMatrix[29,29];
        }

        private double RunBigDTW(double[,] bigDifferenceMatrix)
        {
            double[,] dtwMatrix = new double[30, 30];
            int n = bigDifferenceMatrix.Length;

            for (int i = 0; i < n; i++)
            {
                dtwMatrix[i, 0] = double.MaxValue;
                dtwMatrix[0, i] = double.MaxValue;
            }
            dtwMatrix[0, 0] = 0;

            double cost;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    cost = bigDifferenceMatrix[i,j];
                    dtwMatrix[i, j] = cost + Minimum(dtwMatrix[i - 1, j], dtwMatrix[i, j - 1], dtwMatrix[i - 1, j - 1]);
                }
            }

            return dtwMatrix[n - 1, n - 1];
        }

        private double Minimum(double a, double b, double c)
        {
            if (a > b && a > c) return a;
            if (b > a && b > c) return b;
            return c;
        }

    }
}
