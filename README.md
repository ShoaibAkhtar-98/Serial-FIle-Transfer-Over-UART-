# Serial-FIle-Transfer-Over-UART-
# Serial File Transfer  A reliable file transfer application using serial communication with sliding window protocol and ACK mechanism.  


 Serial File Transfer

A reliable file transfer application using serial communication with sliding window protocol and ACK mechanism.

## Features

- **Reliable Transfer**: Sliding window protocol with ACK mechanism
- **Error Handling**: Timeout detection and retransmission
- **Progress Tracking**: Real-time progress bar and logging
- **Base64 Encoding**: Safe binary data transmission over serial
- **Duplicate Detection**: Prevents processing duplicate packets

## Protocol Specification

### Message Format
All messages are wrapped in delimiters: `<CHUNK>message</CHUNK>`

### Commands
- **START**: `<CHUNK>START|filename|total_chunks</CHUNK>`
- **DATA**: `<CHUNK>DATA|chunk_number|base64_data</CHUNK>`
- **ACK**: `<CHUNK>ACK|first_packet-last_packet</CHUNK>`
- **END**: `<CHUNK>END</CHUNK>`

### Configuration
```csharp
private const int CHUNK_SIZE = 200;     // Bytes per chunk
private const int WINDOW_SIZE = 10;     // Packets per window
private const int BAUD_RATE = 115200;   // Serial baud rate




<img width="786" height="589" alt="image" src="https://github.com/user-attachments/assets/881447e5-0424-41d5-ba71-e3baf56e1184" />
