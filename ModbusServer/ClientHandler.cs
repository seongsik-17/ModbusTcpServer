using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ModbusServer
{
	internal class ClientHandler
	{
		private Action<string> _interfaceUpdate;
		private TcpClient _tcpClient;
		private ModbusParser _parser;

		public ClientHandler(Action<string> interfaceUpdate, TcpClient client)
		{
			this._interfaceUpdate = interfaceUpdate;
			this._tcpClient = client;
		}

		//1. 데이터스토어: 가상 메모리 공간 -> 별도의 static 클래스로 분리했음
		//2. 프레임 파서 및 예외처리 -> 별도의 ModbusParser 클래스로 분리했음
		//3. 스레드 동기화
		//4. 이벤트 발생기
		//5. 고급 세션 관리
		private String getTime()
		{
			return DateTime.Now.ToString("yyyy:MM:dd:hh:mm");
		}
		public void ClientAgent()
		{
			NetworkStream stream = _tcpClient.GetStream();
			byte[] bytes = new byte[256];
			ushort[] response;
			//클라이언트 요청사항 처리하는 로직 만들기
			try
			{
				int bytesRead;
				while ((bytesRead = stream.Read(bytes, 0, bytes.Length)) > 0)
				{
					_interfaceUpdate?.Invoke(getTime() + $" 클라이언트 요청 수신!: {_tcpClient.Client.RemoteEndPoint.ToString()}");
					//파서에서 요청 처리
					stream.ReadTimeout = 3000;
					response = _parser.ProcessRequest(bytes, bytesRead);
					//return response;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(getTime() + $" 클라이언트 처리 중 오류 발생: {ex.Message}");
				stream.Close();
				_tcpClient.Close();
				stream.Dispose();
				_tcpClient.Dispose();
			}
			//return null;
			
		}
	}
}
