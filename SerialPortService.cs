using System;
using System.IO.Ports;

namespace SerialFileTransfer
{
    public class SerialPortService
    {
        private SerialPort _serialPort;

        public event Action<string>? DataReceived;

        public SerialPortService(string portName, int baudRate = 115200)
        {
            _serialPort = new SerialPort(portName, baudRate)
            {
                Encoding = System.Text.Encoding.ASCII,
                ReadTimeout = 2000,
                WriteTimeout = 2000
            };

            _serialPort.DataReceived += (s, e) =>
            {
                try
                {
                    string data = _serialPort.ReadExisting();
                    DataReceived?.Invoke(data);
                }
                catch { }
            };
        }

        public void Open()
        {
            if (!_serialPort.IsOpen)
                _serialPort.Open();
        }

        public void Close()
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();
        }

        public void Send(string data)
        {
            if (_serialPort.IsOpen)
                _serialPort.Write(data);
        }

        public void SendBytes(byte[] bytes)
        {
            if (_serialPort.IsOpen)
                _serialPort.Write(bytes, 0, bytes.Length);
        }
    }
}
