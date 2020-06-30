using System;
using System.Net;
using System.Net.Sockets;

namespace ClientSocketChat
{
	public class ClientWrapper
	{
		public ClientWrapper(TcpClient tcpClient, IPAddress iPAddress, int port)
		{
			this.Id = Guid.NewGuid();
			TcpClient = tcpClient;
			tcpClient.Connect(iPAddress, port);
			this.Stream = tcpClient.GetStream();
		}

		public Guid Id{ get; private set; }

		public TcpClient TcpClient { get; private set; }

		public NetworkStream Stream { get; }
	}
}
