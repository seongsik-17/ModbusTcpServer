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
		private Action<string> _interfaceUpdate;
		private List<ClientHandler> clientList = new List<ClientHandler>();

		public ModbusTcpServer(int port, Action<string> interfaceUpdate)
		{
			_port = port;
			_interfaceUpdate = interfaceUpdate;
			_listener = new TcpListener(IPAddress.Any, _port);
		}

		private String getTime()
		{
			return DateTime.Now.ToString("yyyy:MM:dd:hh:mm");
		}

		public void Start()
		{
			//VirtualMemory.LoadMemory();

			Task.Run(() =>
			{
				try
				{
					_listener.Start();
					//Console.WriteLine("ServerStart!");
					_interfaceUpdate?.Invoke(getTime() + " 서버 시작됨!");

					while (true)
					{
						//연결 대기
						TcpClient tcpClient = _listener.AcceptTcpClient();
						// -> 블로킹함수라 연결이 올 때 까지 대기함
						//Console.WriteLine("클라이언트 연결됨: " + tcpClient.Client.RemoteEndPoint.ToString());
						_interfaceUpdate?.Invoke(getTime() + $" 클라이언트 연결됨: {tcpClient.Client.RemoteEndPoint.ToString()}");
						ClientHandler clientHandler = new ClientHandler(_interfaceUpdate, tcpClient);
						clientList.Add(clientHandler);
						Task.Run(() =>
						{
							clientHandler.ClientAgent();
						});
					}
				}
				catch(SocketException se) when (se.SocketErrorCode == SocketError.Interrupted)
				{
					_interfaceUpdate?.Invoke(getTime() + "서버 종료");
				}
				catch (Exception ex)
				{
					//MessageBox.Show(getTime() + $" 서버 열기 실패!: {ex.Message}");
					_interfaceUpdate?.Invoke(getTime() + $" 서버 열기 실패!: {ex.Message}");
				}
			});
		}

		//모든 자원 해제
		public void Stop()
		{
			try
			{
				//리스너 중지
				_listener.Stop();
				//클라이언트 연결 종료
				foreach (var client in clientList)
				{
					client.Disconnect(); // 클라이언트 연결 종료 메서드 호출
				}
			}
			catch
			{
				
			}
			
		}
	}
}