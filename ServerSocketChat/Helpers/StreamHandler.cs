using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocketChat.Helpers
{
	public class StreamHandler
	{
		public static string ReadByte(NetworkStream stream)
		{
			byte[] buffer = new byte[1024];

			int numBytes = stream.Read(buffer, 0, buffer.Length);
			return numBytes != 0
				? Encoding.UTF8.GetString(buffer, 0, numBytes)
				: string.Empty;
		}

		public static void WriteByte(string message)
		{
			byte[] dataMassege = Encoding.Unicode.GetBytes(message);

			//for (int i = 0; i < clients.Count; i++)
			//{
			//	if (i != id)
			//	{
			//		var stream = clients[i].GetStream();
			//		stream.Write(dataMassege, 0, dataMassege.Length);
			//	}
			//}
		}
	}
}
