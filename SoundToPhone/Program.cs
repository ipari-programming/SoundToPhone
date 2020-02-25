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
            buffer = NoiseReduction(buffer, 200);
            Console.Write("Wav loaded! Press any key to draw...");
            Console.ReadKey();
            DrawH(buffer);
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

                Console.SetCursorPosition(i / unit, Console.WindowHeight - AvgByte(currentSegment) / 5);
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

            for (int i = 0; i < bytes.Count; i++)
            {
                output.Add(bytes[i] > thereshold ? bytes[i] : (byte)0);
            }

            return output;
        }
    }
}
