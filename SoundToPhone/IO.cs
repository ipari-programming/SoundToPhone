using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using NAudio;
using NAudio.Wave;
using NUnit.Framework;

namespace SoundToPhone
{
    public static class IO
    {
        public static List<byte> LoadWav(string fileName)
        {
            byte[] buffer;

            using (WaveFileReader reader = new WaveFileReader(fileName))
            {
                Assert.AreEqual(16, reader.WaveFormat.BitsPerSample, "Only works with 16 bit audio");
                buffer = new byte[reader.Length];
                int read = reader.Read(buffer, 0, buffer.Length);
                short[] sampleBuffer = new short[read / 2];
                Buffer.BlockCopy(buffer, 0, sampleBuffer, 0, read);
            }

            return buffer.ToList();
        }
    }
}
