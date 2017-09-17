using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Boerman.Aeronautics.AprsClient;

namespace Boerman.Aeronautical.AprsMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "APRS Console";

            Listener listener = new Listener();

            StreamWriter w = File.AppendText("positions.txt");
            w.AutoFlush = true;

            listener.PacketReceived += (sender, eventArgs) =>
            {
                // Please note it is faster to use the `listener.DataReceived` event if you only want the raw data.
                Console.WriteLine(eventArgs.AprsMessage.RawData);
                w.Write($"{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)} {eventArgs.AprsMessage.RawData}");
            };

            Console.ReadKey();
        }
    }
}
