using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace Vecc.Netduino.Drivers.Tmp102Sensor
{
    public class Sensor : IDisposable
    {
        //WIRING:

        /*
         * TMP102               NETDUINO
         * ADD0                 GND
         * ALT
         * SCL                  SCL/SC
         * SDA                  SDA/SD
         * GND                  GND
         * VCC                  3V3
         */

        private readonly int _i2CAddress;
        private readonly byte[] _readBuffer = new byte[2];
        private readonly int _i2CClockRateKHz;
        private readonly I2CDevice _tmp102;

        private double _result;
        private bool _disposed = false;

        public Sensor(int address, int clockRateKHz = 400)
        {
            _i2CAddress = address;
            _i2CClockRateKHz = clockRateKHz;

            _tmp102 = new I2CDevice(new I2CDevice.Configuration((ushort)_i2CAddress, _i2CClockRateKHz));
        }

        public double GetTemperature()
        {
            lock (_tmp102)
            {
                _readBuffer[0] = 0;
                _readBuffer[1] = 0;
                var reading = new I2CDevice.I2CTransaction[] { I2CDevice.CreateReadTransaction(_readBuffer) };
                var bytesRead = _tmp102.Execute(reading, 1000);
                var readVal = ((_readBuffer[0] << 8) | _readBuffer[1]) >> 4;
                _result = readVal * 0.0625;
                return _result;
            }

        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    if (_tmp102 != null)
                    {
                        _tmp102.Dispose();
                    }
                }
                _disposed = true;
            }
        }
    }
}
