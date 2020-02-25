using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundToPhone
{
    class Program
    {
        static void Main(string[] args)
        {
            List<byte> buffer = IO.LoadWav("../../../telefon.wav");
            buffer = NoiseReduction(buffer, 100);
            List<int> lengths = GetLengths(buffer, 100);

            Console.ReadKey();
        }

        static void DrawH(List<byte> buffer)
        {
            int unit = buffer.Count / Console.WindowWidth;
            List<byte> currentSegment;

            for (int i = 0; i < buffer.Count; i += unit)
            {
                if (i / unit + 1 >= Console.WindowWidth) break;

                if (i + unit > buffer.Count) currentSegment = buffer.GetRange(i, buffer.Count - i);
                else currentSegment = buffer.GetRange(i, unit);

                Console.SetCursorPosition(i / unit, Console.WindowHeight - AvgByte(currentSegment) / 7);
                Console.Write(AvgByte(currentSegment) / 5);
            }
        }

        static byte AvgByte(List<byte> bytes)
        {
            return (byte)bytes.Average((byte input) => { return input; });
        }

        static List<byte> NoiseReduction(List<byte> bytes, byte thereshold)
        {
            List<byte> output = new List<byte>();

            for (int x = 0; x < 3; x++)
            {
                for (int i = 1; i < bytes.Count - 1; i++)
                {
                    if (bytes[i - 1] > thereshold && bytes[i + 1] > thereshold && bytes[i] < thereshold)
                    {
                        // Don't add
                    }
                    else
                    {
                        output.Add(bytes[i] > thereshold ? bytes[i] : (byte)0);
                    }
                }
            }

            Console.WriteLine($"Noise reduction removed {bytes.Count - output.Count} bytes.");
            Console.ReadKey();

            return output;
        }

        static List<int> GetLengths(List<byte> bytes, int thereshold)
        {
            List<int> lengths = new List<int>();

            if (bytes[0] >= thereshold) lengths.Add(0);

            for (int i = 0; i < bytes.Count; i++)
            {
                if (bytes[i] < thereshold)
                {
                    lengths.Add(0);
                }
                else
                {
                    lengths[lengths.Count - 1]++;
                }
            }

            lengths.RemoveAll(r => r < 2);

            return lengths;
        }
    }
}
