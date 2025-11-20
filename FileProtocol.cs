using System;
using System.IO;
using System.Text;

namespace SerialFileTransfer
{
    public class FileProtocol
    {
        public void SendFile(string filePath, SerialPortService serial, Action<double> progressCallback)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            string fileName = Path.GetFileName(filePath);
            int chunkSize = 1024;
            int totalChunks = (int)Math.Ceiling((double)fileBytes.Length / chunkSize);

            // Send File Info Pack
            string header = $"##SENDERRECV00010x03{fileName}.bin{fileBytes.Length}{totalChunks}!!";
            serial.Send(header);

            for (int i = 0; i < totalChunks; i++)
            {
                int offset = i * chunkSize;
                int remaining = Math.Min(chunkSize, fileBytes.Length - offset);
                byte[] chunk = new byte[remaining];
                Array.Copy(fileBytes, offset, chunk, 0, remaining);

                // File Chunk Pack
                string chunkHeader = $"##SENDERRECV00010x04{i:D4}";
                byte[] headerBytes = Encoding.ASCII.GetBytes(chunkHeader);
                byte[] footer = Encoding.ASCII.GetBytes("!!");

                byte[] packet = new byte[headerBytes.Length + chunk.Length + footer.Length];
                Buffer.BlockCopy(headerBytes, 0, packet, 0, headerBytes.Length);
                Buffer.BlockCopy(chunk, 0, packet, headerBytes.Length, chunk.Length);
                Buffer.BlockCopy(footer, 0, packet, headerBytes.Length + chunk.Length, footer.Length);

                serial.SendBytes(packet);

                if (i % 10 == 0)
                    serial.Send($"##ACK{i}!!"); // acknowledgment simulation

                double percent = ((i + 1) / (double)totalChunks) * 100;
                progressCallback(percent);
            }

            serial.Send("##EOT!!"); // End of Transmission
        }
    }
}
