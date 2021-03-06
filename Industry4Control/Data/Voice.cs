﻿using Industry4Control.Utils;
using Industry4Control.Constants;
using System;

namespace Industry4Control.Data
{
    [Serializable]
    internal class Voice
    {
        public ParameterVector[] ParameterVectors;
        public int LowerMostIndex;
        public int UpperMostIndex;

        public int Length
        {
            get
            {
                return UpperMostIndex - LowerMostIndex;
            }
        }

        public Voice(short[] voice)
        {
            /*byte[] byteArray = new byte[voice.Length * 2];
            Buffer.BlockCopy(voice, 0, byteArray, 0, byteArray.Length);

            SoundPlayer player = new SoundPlayer(new MemoryStream(byteArray));

            player.Play();*/

            ParameterVectors = Helper.CreateParameterVectors(voice);
            for(int i = 0; i < ParameterVectors.Length; i++)
            {
                if(ParameterVectors[i].GetSum() >= Limits.VoiceAmplitudeLimit)
                {
                    LowerMostIndex = i;
                    break;
                }
            }

            for (int i = ParameterVectors.Length - 1; i > 0; i--)
            {
                if (ParameterVectors[i].GetSum() >= Limits.VoiceAmplitudeLimit)
                {
                    UpperMostIndex = i;
                    break;
                }
            }
        }
    }
}
