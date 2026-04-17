using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ModbusServer
{
	internal class ModbusTcpServer
	{
		private int _port;
		private TcpListener _listener;
		private Action<string> _updateAction;

		public ModbusTcpServer(int port, Action<string> updateAction)
		{
			_port = port;
			_updateAction = updateAction;
			_listener = new TcpListener(IPAddress.Any, _port);
		}
		private String getTime()
		{
			return DateTime.Now.ToString("yyyy:MM:dd:hh:mm");
		}
	
		public void Start()
		{
			Task.Run(() =>
			{
				try
				{
					_listener.Start();
					//Console.WriteLine("ServerStart!");
					_updateAction?.Invoke(getTime() + " 서버 시작됨!");
				}
				catch (Exception ex)
				{
					MessageBox.Show(getTime() + $" 서버 열기 실패!: {ex.Message}");
					_updateAction?.Invoke(getTime() + $" 서버 열기 실패!: {ex.Message}");
				}

				while (true)
				{
					//연결 대기
					TcpClient tcpClient = _listener.AcceptTcpClient();
					// -> 블로킹함수라 연결이 올 때 까지 대기함
					//Console.WriteLine("클라이언트 연결됨: " + tcpClient.Client.RemoteEndPoint.ToString());
					_updateAction?.Invoke(getTime() + $" 클라이언트 연결됨: {tcpClient.Client.RemoteEndPoint.ToString()}");
					ClientHandler clientHandler = new ClientHandler(_updateAction, tcpClient);
					Task.Run(() =>
					{
						clientHandler.ClientAgent();
					});
				}
			});
		}
		//별도의 클래스로 분리할 예정
		//private void ClientHandler(TcpClient client)
		//{
		//	NetworkStream stream = client.GetStream();
		//	byte[] bytes = new byte[256];
		//	//클라이언트 요청사항 처리하는 로직 만들기
		//	try
		//	{
		//		int bytesRead;
		//		while ((bytesRead = stream.Read(bytes, 0, bytes.Length)) > 0)
		//		{
		//			stream.ReadTimeout = 3000;
		//			_updateAction?.Invoke(getTime() + $" 클라이언트 요청 수신!");
		//		}
		//	}
		//	catch(Exception ex)
		//	{
		//		MessageBox.Show(getTime() + $" 클라이언트 처리 중 오류 발생: {ex.Message}");
		//		stream.Close();
		//		client.Close();
		//		stream.Dispose();
		//		client.Dispose();
		//	}

		//}
	}
}