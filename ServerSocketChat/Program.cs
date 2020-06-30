using System;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ServerSocketChat
{
    class Program
    {
        private static int port = 443;
        private static string ipAdress = "127.0.0.1";
        static X509Certificate serverCertificate = null;
        public static Hashtable clients = new Hashtable();

        static void Main(string[] args)
        {
            serverCertificate = X509Certificate.CreateFromCertFile(@"D:\Certificate 2018\socketChat.crt");
            TcpListener tcpListener = new TcpListener(new IPEndPoint(IPAddress.Parse(ipAdress), port));
            tcpListener.Start(5);
           
            TcpClient clientSocket = tcpListener.AcceptTcpClient();
            SslStream sslStream = new SslStream(clientSocket.GetStream(), true);
            sslStream.AuthenticateAsServer(serverCertificate, false, SslProtocols.Tls12, true);
         
            while (true)
            {
                try
                {
                    ReadData(sslStream);
                    SendData(sslStream);
                }
                catch (AuthenticationException e)
                {
                    Console.WriteLine($"exception: {e.Message}");
                    Disconnect(clientSocket, sslStream);
                }
            }
        }

        private static void ReadData(SslStream sslStream)
        {
            var buffer = new byte[256];
            var message = new StringBuilder();
            var data = sslStream.Read(buffer, 0, buffer.Length);

            if (data > 0)
            {
                message.Append(Encoding.UTF8.GetString(buffer, 0, data));
            }
            Console.WriteLine($"message from client received {message}");
        }

        private static void SendData(SslStream sslStream)
        {
            if (sslStream.CanWrite)
            {
                var response = Encoding.UTF8.GetBytes($"your message received successfully");
                sslStream.Write(response, 0, response.Length);
            }
        }

        private static void Disconnect(TcpClient tcpSocket, SslStream sslStream)
        {
            sslStream.Close();
            tcpSocket.Close();
        }
    }
}
