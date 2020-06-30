using System;

namespace ServerSocketChat.Models
{
	[Serializable]
	public class ChatInfoParameters
	{
		public string Sender { get; set; }

		public string Reciever { get; set; }

		public string Message { get; set; }

		public bool IsRead { get; set; }
	}
}
