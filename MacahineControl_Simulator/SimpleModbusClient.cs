using System;
using System.Net.Sockets;

namespace MacahineControl_Simulator
{
    public class SimpleModbusClient : IDisposable
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private ushort _transactionId = 0;

        public bool IsConnected => _client != null && _client.Connected;

        public async Task<bool> ConnectAsync(string ip, int port)
        {
            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(ip, port);
                _stream = _client.GetStream();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Disconnect()
        {
            _stream?.Close();
            _client?.Close();
        }

        public async Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort quantity)
        {
            if (!IsConnected) return null;

            _transactionId++;
            byte[] request = new byte[12];
            // MBAP Header
            request[0] = (byte)(_transactionId >> 8);
            request[1] = (byte)(_transactionId & 0xFF);
            request[2] = 0; // Protocol ID
            request[3] = 0;
            request[4] = 0; // Length
            request[5] = 6;
            request[6] = 1; // Unit ID

            // PDU
            request[7] = 0x03; // Function Code
            request[8] = (byte)(startAddress >> 8);
            request[9] = (byte)(startAddress & 0xFF);
            request[10] = (byte)(quantity >> 8);
            request[11] = (byte)(quantity & 0xFF);

            await _stream.WriteAsync(request, 0, request.Length);

            byte[] responseHeader = new byte[9];
            await _stream.ReadExactlyAsync(responseHeader, 0, 9);

            int byteCount = responseHeader[8];
            byte[] data = new byte[byteCount];
            await _stream.ReadExactlyAsync(data, 0, byteCount);

            ushort[] values = new ushort[quantity];
            for (int i = 0; i < quantity; i++)
            {
                values[i] = (ushort)((data[i * 2] << 8) | data[i * 2 + 1]);
            }
            return values;
        }

        public async Task<bool> WriteMultipleRegistersAsync(ushort startAddress, ushort[] values)
        {
            if (!IsConnected) return false;

            _transactionId++;
            int byteCount = values.Length * 2;
            byte[] request = new byte[13 + byteCount];

            // MBAP Header
            request[0] = (byte)(_transactionId >> 8);
            request[1] = (byte)(_transactionId & 0xFF);
            request[2] = 0;
            request[3] = 0;
            ushort length = (ushort)(7 + byteCount);
            request[4] = (byte)(length >> 8);
            request[5] = (byte)(length & 0xFF);
            request[6] = 1;

            // PDU
            request[7] = 0x10; // Function Code
            request[8] = (byte)(startAddress >> 8);
            request[9] = (byte)(startAddress & 0xFF);
            request[10] = (byte)(values.Length >> 8);
            request[11] = (byte)(values.Length & 0xFF);
            request[12] = (byte)byteCount;

            for (int i = 0; i < values.Length; i++)
            {
                request[13 + i * 2] = (byte)(values[i] >> 8);
                request[14 + i * 2] = (byte)(values[i] & 0xFF);
            }

            await _stream.WriteAsync(request, 0, request.Length);

            byte[] response = new byte[12];
            await _stream.ReadExactlyAsync(response, 0, 12);

            return response[7] == 0x10;
        }

        public void Dispose()
        {
            Disconnect();
            _stream?.Dispose();
            _client?.Dispose();
        }
    }
}
