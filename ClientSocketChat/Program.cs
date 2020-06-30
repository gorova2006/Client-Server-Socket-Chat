using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;

namespace ClientSocketChat
{
    class Program
    {
        private static int port = 443;
        private static string ipAdress = "127.0.0.1";

        static void Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient(ipAdress, port);
            SslStream sslStream = new SslStream(
                tcpClient.GetStream(),
                true,
                new RemoteCertificateValidationCallback((obj, cert, chain, error) =>
                {
                   return error == SslPolicyErrors.None;
                }), null);
            
            try
            {
                sslStream.AuthenticateAsClient("ws03.i.sigmaukraine.com");
                while (true)
                {
                    SendData(sslStream, tcpClient);
                    ReadData(sslStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SendData(SslStream sslStream, TcpClient tcpClient)
        {
            var message = GetMessage();

            if (sslStream.CanWrite)
            {
                var data = Encoding.UTF8.GetBytes(message);
                sslStream.Write(data, 0, data.Length);
            }
            else
            {
                Console.WriteLine("You cannot write data to this stream.");
                sslStream.Close();
                tcpClient.Close();

                return;
            }
        }

        private static void ReadData(SslStream sslStream)
        {
            var buffer = new byte[256];

            int bytesRead = sslStream.Read(buffer, 0, buffer.Length);
            var response = (Encoding.UTF8.GetString(buffer, 0, bytesRead));
            Console.WriteLine(response);
        }

        private static string GetMessage()
        {
            Console.Write("Enter your message (ESC to cancel)");
            ConsoleKeyInfo info = Console.ReadKey(true);

            if (info.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }

            return Console.ReadLine();
        }
    }
}

