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

        private int m_LowerIndex;
        private int m_UpperIndex;

        public CustomDtw(Voice voice)
        {
            m_Voice = voice;
        }

        public double GetDtwValue(Voice voice)
        {
            double result = double.MaxValue;

            m_LowerIndex = (m_Voice.LowerMostIndex < voice.LowerMostIndex) ? m_Voice.LowerMostIndex : voice.LowerMostIndex;
            m_UpperIndex = (m_Voice.UpperMostIndex > voice.UpperMostIndex) ? m_Voice.UpperMostIndex : voice.UpperMostIndex;

            int diff = Math.Abs(voice.Length - m_Voice.Length);

            if (diff > 8)
            {
                return double.MaxValue;
            }

            if (m_UpperIndex <= m_LowerIndex)
            {
                return double.MaxValue;
            }

            double[,] dtwResultMatrix = new double[m_UpperIndex - m_LowerIndex, m_UpperIndex - m_LowerIndex];

            for (int i = 0; i < m_UpperIndex - m_LowerIndex; i++)
            {
                for (int j = 0; j < m_UpperIndex - m_LowerIndex; j++)
                {
                    double smallDtwResult = RunSmallDtw(m_Voice.ParameterVectors[m_LowerIndex + i], voice.ParameterVectors[m_LowerIndex + j], i, j);
                    dtwResultMatrix[i, j] = smallDtwResult;
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

            result = RunBigDTW(dtwResultMatrix);

            m_DtwResultMatrix = dtwResultMatrix;

            return result;
        }

        public bool IsMatch(Voice voice)
        {
            double result = GetDtwValue(voice);

            if (result < Limits.GetLimit(m_UpperIndex - m_LowerIndex))
            {
                return true;
            }

            return false;
        }

        public double[,] GetDtwMatrix()
        {
            return m_DtwResultMatrix;
        }


        // Done
        private double RunSmallDtw(ParameterVector vector1, ParameterVector vector2, int x, int y)
        {
            short[] melSpectrum1 = vector1.GetMelSpectrum();
            short[] melSpectrum2 = vector2.GetMelSpectrum();

            double[,] distances = new double[30, 30];
            for(int i = 0; i < 30; i++)
            {
                for(int j = 0; j < 30; j++)
                {
                    if(Math.Abs(i-j) > 20)
                    {
                        distances[i, j] = short.MaxValue; // ???
                    }
                    else
                    {
                        double diff = Math.Abs(melSpectrum1[i] - melSpectrum2[j]);
                        distances[i, j] = diff * diff;
                    }
                }
            }

            double[,] dtwMatrix = new double[30,30];
            dtwMatrix[0, 0] = 0;
            double max = 1;
            for (int i = 1; i < 30; i++)
            {
                dtwMatrix[i, 0] = distances[i, 0] + dtwMatrix[i - 1, 0];
                dtwMatrix[0, i] = distances[0, i] + dtwMatrix[0, i - 1];

                if (dtwMatrix[i, 0] > max) max = dtwMatrix[i, 0];
                if (dtwMatrix[0, i] > max) max = dtwMatrix[0, i];
            }
            
            for(int i = 1; i < 30; i++)
            {
                for(int j = 1; j < 30; j++)
                {
                    dtwMatrix[i, j] = distances[i,j] + Minimum(dtwMatrix[i - 1, j], dtwMatrix[i, j - 1], dtwMatrix[i - 1, j - 1]);
                    if(dtwMatrix[i, j] > max)
                    {
                        max = dtwMatrix[i, j];
                    }
                }
            }
            
            // Scaling the values
           /* for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    dtwMatrix[i, j] =(dtwMatrix[i, j] / max) * 100;
                }
            }*/

            using (StreamWriter writer = new StreamWriter("smallDtw\\smallDtw"+x+"_"+y+".txt"))
            {
                for (int i = 0; i < dtwMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < dtwMatrix.GetLength(0); j++)
                    {
                        writer.Write(String.Format("[{0,8:#########}]", dtwMatrix[i, j]));
                    }
                    writer.Write("\n");
                }
            }

            /*int k = 29, l = 29;
            double cost = 0;

            while ( k > 0 && l > 0 )
            {
                if (k == 0)
                {
                    l--;
                }else if(l == 0)
                {
                    k--;
                }
                else
                {
                    if(dtwMatrix[k - 1, l] == Minimum(dtwMatrix[k - 1, l - 1], dtwMatrix[k - 1, l], dtwMatrix[k, l - 1]))
                    {
                        k--;
                    }else if(dtwMatrix[k, l - 1] == Minimum(dtwMatrix[k - 1, l - 1], dtwMatrix[k - 1, l], dtwMatrix[k, l - 1]))
                    {
                        l--;
                    }
                    else
                    {
                        k--;
                        l--;
                    }
                }
                cost += dtwMatrix[k,l];
            }

            return cost;*/

            return dtwMatrix[29, 29];
        }


        private double RunBigDTW(double[,] bigDifferenceMatrix)
        {
            int x = bigDifferenceMatrix.GetLength(0);
            
            double[,] dtwMatrix = new double[x, x];
            dtwMatrix[0, 0] = 0;

            for (int i = 1; i < x; i++)
            {
                dtwMatrix[i, 0] = dtwMatrix[i - 1, 0] + bigDifferenceMatrix[i, 0];
                dtwMatrix[0, i] = dtwMatrix[0, i - 1] + bigDifferenceMatrix[0, i];
            }

            int radius = (x / 3) * 2;
            for (int i = 1; i < x; i++)
            {
                for(int j = 1; j < x; j++)
                {
                    /*if(Math.Abs(i-j) > radius)
                    {
                        dtwMatrix[i, j] = 1000000;
                    }
                    else
                    {*/
                        dtwMatrix[i, j] = bigDifferenceMatrix[i, j] + Minimum(dtwMatrix[i - 1, j], dtwMatrix[i, j - 1], dtwMatrix[i - 1, j - 1]);
                    //}
                }
            }

            IList<Point> points = new List<Point>();
            /*double cost = 0;
            int k = x - 1, l = x - 1;
            while (k > 0 && l > 0)  
            {
                if (k == 0)
                {
                    l--;
                }
                else if (l == 0)
                {
                    k--;
                }
                else
                {
                    if (dtwMatrix[k - 1, l] == Minimum(dtwMatrix[k - 1, l], dtwMatrix[k, l - 1], dtwMatrix[k - 1, l - 1]))
                    {
                        k--;
                    }
                    else if (dtwMatrix[k, l - 1] == Minimum(dtwMatrix[k - 1, l], dtwMatrix[k, l - 1], dtwMatrix[k - 1, l - 1]))
                    {
                        l--;
                    }
                    else
                    {
                        k--;
                        l--;
                    }
                }
                cost += dtwMatrix[k, l];
                points.Add(new Point(k, l));
            }*/

#if DEBUG

            using (StreamWriter writer = new StreamWriter("bigDtw.txt"))
            {
                writer.Write(String.Format("steps: {0}, size: {1} \n", points.Count, bigDifferenceMatrix.GetLength(0)));

                for (int i = 0; i < bigDifferenceMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < bigDifferenceMatrix.GetLength(0); j++)
                    {
                        if(points.Any(a => a.X == i && a.Y == j))
                        {
                            writer.Write(String.Format("[{0,6:#####}]", dtwMatrix[i, j]));
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

            //return cost;
            return dtwMatrix[x-1, x-1];
        }

        private double Minimum(double a, double b, double c)
        {
            if (a < b && a < c) return a;
            if (b < a && b < c) return b;
            return c;
        }

    }
}
