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

        public Voice Voice { get { return m_Voice; } }

        public CustomDtw(Voice voice)
        {
            m_Voice = voice;
        }

        public Tuple<double, int> GetDtwValue(Voice voice)
        {
            double result = double.MaxValue;

            m_LowerIndex = (m_Voice.LowerMostIndex < voice.LowerMostIndex) ? m_Voice.LowerMostIndex : voice.LowerMostIndex;
            m_UpperIndex = (m_Voice.UpperMostIndex > voice.UpperMostIndex) ? m_Voice.UpperMostIndex : voice.UpperMostIndex;

            int diff = Math.Abs(voice.Length - m_Voice.Length);

            if (diff > 8)
            {
                return null;
            }

            if (m_UpperIndex <= m_LowerIndex)
            {
                return null;
            }

            double[,] dtwResultMatrix = new double[m_UpperIndex - m_LowerIndex, m_UpperIndex - m_LowerIndex];

            var watch1 = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < m_UpperIndex - m_LowerIndex; i++)
            {
                for (int j = 0; j < m_UpperIndex - m_LowerIndex; j++)
                {
                    double smallDtwResult = RunSmallDtw(m_Voice.ParameterVectors[m_LowerIndex + i], voice.ParameterVectors[m_LowerIndex + j]);
                    dtwResultMatrix[i, j] = smallDtwResult;
                }
            }

            watch1.Stop();
            Console.WriteLine("Small DTW calculation time: " + watch1.ElapsedMilliseconds);

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

            var watch2 = System.Diagnostics.Stopwatch.StartNew();

            Tuple<double, int> results = RunDtw(dtwResultMatrix);

            //result = RunBigDTW(dtwResultMatrix);

            watch2.Stop();

            Console.WriteLine("Big DTW calculation time: " + watch2.ElapsedMilliseconds);

            m_DtwResultMatrix = dtwResultMatrix;

            return results;
        }

        public Tuple<double, int> Compare(Voice voice)
        {
            return GetDtwValue(voice);
        }

        public double[,] GetDtwMatrix()
        {
            return m_DtwResultMatrix;
        }


        // Done
        private double RunSmallDtw(ParameterVector vector1, ParameterVector vector2)
        {
            short[] melSpectrum1 = vector1.GetMelSpectrum();
            short[] melSpectrum2 = vector2.GetMelSpectrum();

            double[,] distances = new double[30, 30];
            for(int i = 0; i < 30; i++)
            {
                for(int j = 0; j < 30; j++)
                {
                    if(Math.Abs(i-j) > 18)
                    {
                        distances[i, j] = short.MaxValue; // ???
                        continue;
                    }

                    double diff = Math.Abs(melSpectrum1[i] - melSpectrum2[j]);
                    distances[i, j] = diff * diff;
                }
            }

            Tuple<double, int> result = RunDtw(distances);

            #region Commented

            /*double[,] dtwMatrix = new double[30,30];
            dtwMatrix[0, 0] = 0;
            double max = 1;
            for (int i = 1; i < 19; i++)
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
                    if(dtwMatrix[i,j] != short.MaxValue)
                    {
                        dtwMatrix[i, j] = distances[i, j] + Minimum(dtwMatrix[i - 1, j], dtwMatrix[i, j - 1], dtwMatrix[i - 1, j - 1]);
                        if (dtwMatrix[i, j] > max)
                        {
                            max = dtwMatrix[i, j];
                        }
                    }
                }
            }

            // Scaling the values
            for (int i = 0; i < 30; i++)
            {
                 for (int j = 0; j < 30; j++)
                 {
                     dtwMatrix[i, j] =(dtwMatrix[i, j] / max) * 100;
                 }
            }*/

#if DEBUG
            /*using (StreamWriter writer = new StreamWriter("smallDtw\\smallDtw"+x+"_"+y+".txt"))
            {
                for (int i = 0; i < dtwMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < dtwMatrix.GetLength(0); j++)
                    {
                        writer.Write(String.Format("[{0,8:#########}]", dtwMatrix[i, j]));
                    }
                    writer.Write("\n");
                }
            }*/
#endif

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

            #endregion

            return result.Item1;
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

#if DEBUG

            IList<Point> points = new List<Point>();
            double cost = 0;
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
            }



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


        private Tuple<double, int> RunDtw(double[,] distanceMatrix)
        {
            int steps = 99999; ;

            if(distanceMatrix == null ||
                distanceMatrix.GetLength(0) == 0 ||
                distanceMatrix.GetLength(1) == 0 ||
                distanceMatrix.GetLength(0) != distanceMatrix.GetLength(1))
            {
                return null;
            }

            int length = distanceMatrix.GetLength(0);
            double[,] dtwMatrix = new double[length,length];
            double max = 0;

            for(int index = 1; index < length; index++)
            {
                dtwMatrix[index, 0] = distanceMatrix[index, 0] + dtwMatrix[index - 1, 0];
                dtwMatrix[0, index] = distanceMatrix[0, index] + dtwMatrix[0, index - 1];

                if (dtwMatrix[index, 0] > max) max = dtwMatrix[index, 0];
                if (dtwMatrix[0, index] > max) max = dtwMatrix[0, index];
            }

            for(int x = 1; x < length; x++)
            {
                for(int y = 1; y < length; y++)
                {
                    dtwMatrix[x, y] = distanceMatrix[x, y] + Minimum(dtwMatrix[x - 1, y], dtwMatrix[x, y - 1], dtwMatrix[x - 1, y - 1]);

                    if(dtwMatrix[x,y] > max)
                    {
                        max = dtwMatrix[x, y];
                    }
                }
            }

            // Scaling the values
            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    dtwMatrix[x, y] = (dtwMatrix[x, y] / max) * 100;
                }
            }


            steps = 0;
            int i = length-1, j = length-1;
            while (i > 0 && j > 0)  
            {
                if (i == 0)
                {
                    j--;
                }
                else if (j == 0)
                {
                    i--;
                }
                else
                {
                    if (dtwMatrix[i - 1, j] == Minimum(dtwMatrix[i - 1, j], dtwMatrix[i, j - 1], dtwMatrix[i - 1, j - 1]))
                    {
                        i--;
                    }
                    else if (dtwMatrix[i, j - 1] == Minimum(dtwMatrix[i - 1, j], dtwMatrix[i, j - 1], dtwMatrix[i - 1, j - 1]))
                    {
                        j--;
                    }
                    else
                    {
                        i--;
                        j--;
                    }
                }

                steps++;
            }

            return new Tuple<double, int>(dtwMatrix[length - 1, length - 1], steps);
        }

    }
}
