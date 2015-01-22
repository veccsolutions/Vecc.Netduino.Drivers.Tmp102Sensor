using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Vecc.Netduino.Drivers.Tmp102Sensor.Sandbox
{
    public class Program
    {
        public static void Main()
        {
            var sensor = new Drivers.Tmp102Sensor.Sensor(0x48);
            var temperature = sensor.GetTemperature();
            var fahrenheit = temperature * 1.8 + 32;

            Debug.Print("C: " + Format(temperature));
            Debug.Print("F: " + Format(fahrenheit));
        }

        private static string Format(double value)
        {
            var wholeNumber = (int)System.Math.Floor(value);
            var decimalPortion = value - wholeNumber;
            var decimals = ((int)System.Math.Floor(decimalPortion * 1000D)).ToString();

            while (decimals.Length < 3)
            {
                decimals += "0";
            }

            return wholeNumber + "." + decimals;
        }
    }
}
