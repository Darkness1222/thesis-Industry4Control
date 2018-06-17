using Industry4Control.Constants;
using Industry4Control.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Industry4Control.BusinessLogic
{
    internal class CustomDtw
    {
        private const double DTWLIMIT = 900.0;
        private Voice m_Voice;
        private double[,] m_DtwResultMatrix;

        public CustomDtw(Voice voice)
        {
            m_Voice = voice;
        }

        public bool IsMatch(Voice voice)
        {
            int lowerIndex = (m_Voice.LowerMostIndex < voice.LowerMostIndex) ? m_Voice.LowerMostIndex : voice.LowerMostIndex;
            int upperIndex = (m_Voice.UpperMostIndex > voice.UpperMostIndex) ? m_Voice.UpperMostIndex : voice.UpperMostIndex;

            if (Math.Abs(voice.Length - m_Voice.Length) > 15)
                return false;

            if (upperIndex < lowerIndex)
                return false;

            double[,] dtwResultMatrix = new double[upperIndex - lowerIndex, upperIndex - lowerIndex];

            for(int i = 0; i < upperIndex - lowerIndex; i++)
            {
                for(int j = 0; j < upperIndex - lowerIndex; j++)
                {
                    dtwResultMatrix[i, j] = RunSmallDtw(m_Voice.ParameterVectors[lowerIndex + i], voice.ParameterVectors[lowerIndex + j]);
                }
            }

#if DEBUG

            using (StreamWriter writer = new StreamWriter("bigDiffMatrix.txt"))
            {
                for (int i = 0; i < dtwResultMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < dtwResultMatrix.GetLength(1); j++)
                    {
                        writer.Write(String.Format("[{0,8:#########}]", dtwResultMatrix[i, j]));
                    }
                    writer.Write("\n");
                }
            }

#endif

            double result = RunBigDTW(dtwResultMatrix);

            if(result < Limits.DtwLimit)
            {
                return true;
            }

            m_DtwResultMatrix = dtwResultMatrix;

            return false;
        }

        public double[,] GetDtwMatrix()
        {
            return m_DtwResultMatrix;
        }

        private double RunSmallDtw(ParameterVector vector1, ParameterVector vector2)
        {
            double[] melSpectrum1 = vector1.GetMelSpectrum();
            double[] melSpectrum2 = vector2.GetMelSpectrum();

            double[,] dtwMatrix = new double[30,30];
            for (int i = 0; i < 30; i++)
            {
                dtwMatrix[i, 0] = double.MaxValue;
                dtwMatrix[0, i] = double.MaxValue;
            }

            dtwMatrix[0, 0] = 0;
            double cost;
            for(int i = 1; i < 30; i++)
            {
                for(int j = 1; j < 30; j++)
                {
                    if(Math.Abs(i-j) > 20)
                    {
                        dtwMatrix[i, j] = 10000;
                    }
                    else{
                        cost = Math.Abs(melSpectrum1[i] - melSpectrum2[j]);
                        dtwMatrix[i, j] = cost + Minimum(dtwMatrix[i - 1, j], dtwMatrix[i, j - 1], dtwMatrix[i - 1, j - 1]);
                    }
                }
            }

            return dtwMatrix[29, 29];
        }

        private double RunBigDTW(double[,] bigDifferenceMatrix)
        {
            double[,] dtwMatrix = new double[bigDifferenceMatrix.GetLength(0), bigDifferenceMatrix.GetLength(1)];
            for (int i = 0; i < bigDifferenceMatrix.GetLength(0); i++)
            {
                dtwMatrix[i, 0] = 10000;
                dtwMatrix[0, i] = 10000;
            }
            dtwMatrix[0, 0] = 0;

            int radius = (bigDifferenceMatrix.GetLength(0) / 3) * 2;
            for (int i = 1; i < bigDifferenceMatrix.GetLength(0); i++)
            {
                for(int j = 1; j < bigDifferenceMatrix.GetLength(1); j++)
                {
                    if(Math.Abs(i-j) > radius)
                    {
                        dtwMatrix[i, j] = 10000;
                    }
                    else
                    {
                        dtwMatrix[i, j] = bigDifferenceMatrix[i, j] + Minimum(dtwMatrix[i - 1, j], dtwMatrix[i, j - 1], dtwMatrix[i - 1, j - 1]);
                    }
                }
            }

            IList<Point> points = new List<Point>();
            double cost = 0;
            int k = bigDifferenceMatrix.GetLength(0) - 1, l = bigDifferenceMatrix.GetLength(1) - 1;
            while (k > 0 && l > 0)  
            {
                double a = double.MaxValue;
                double b = double.MaxValue;
                double c = double.MaxValue;
                if (k > 0)
                {
                    a = dtwMatrix[k - 1, l];
                }

                if (l > 0)
                {
                    b = dtwMatrix[k, l - 1];
                }

                if (k > 0 && l > 0)
                {
                    c = dtwMatrix[k - 1, l - 1];
                }

                double min = Minimum(a, b, c);
                if (min == a)
                {
                    if (k - 1 >= 0)
                        k -= 1;
                }
                if (min == b)
                {
                    if (l - 1 >= 0)
                        l -= 1;
                }
                if (min == c)
                {
                    if (k - 1 >= 0)
                        k -= 1;
                    if (l - 1 >= 0)
                        l -= 1;
                }
                points.Add(new Point(k, l));
                cost += min;
            }

#if DEBUG

            using (StreamWriter writer = new StreamWriter("bigDtw.txt"))
            {
                for (int i = 0; i < bigDifferenceMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < bigDifferenceMatrix.GetLength(1); j++)
                    {
                        if(points.Any(a => a.X == i && a.Y == j))
                        {
                            writer.Write(String.Format("[{0,8:#########}]", dtwMatrix[i, j]));
                        }
                        else
                        {
                            writer.Write(String.Format("{0,8:#########}", dtwMatrix[i, j]));
                        }
                        
                        if (i == j)
                            writer.Write(" ");
                    }
                    writer.Write("\n");
                }
            }

#endif

            return cost;
        }

        private double Minimum(double a, double b, double c)
        {
            if (a < b && a < c) return a;
            if (b < a && b < c) return b;
            return c;
        }

    }
}
