using System.Net.Sockets;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Lab1_ServerApp
{
    internal class Program
    {
        public static void GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine($"SERVER- IP Address: {ip.ToString()}");
                }
            }
            throw new Exception("SERVER- No network adapters with an IPv4 address in the system!");
        }
        static void ProcessMessage(object parm)
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.SetMinimumLevel(LogLevel.Information)); // Set to Information level
            ILogger logger = factory.CreateLogger("Program");

            string data;
            int count;
            try
            {
               
                TcpClient client = parm as TcpClient;
                Byte[] bytes = new Byte[256];
                NetworkStream stream = client.GetStream();
                while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, count);
                    Console.WriteLine($"SERVER- Received data:{data} at {DateTime.Now:t}");
                    data = $"{data.ToUpper()}";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine($"SERVER- Sent data: {data}");
                }
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
                Console.WriteLine("SERVER- Waiting another message...");
            }
        }

        static void ExecuteServer(string host, int port)
        {
            int Count = 0;
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse(host);
                server = new TcpListener(localAddr, port); // Start listening for client requests.
                server.Start();
                Console.WriteLine(new string('*', 40));
                Console.WriteLine("SERVER- Waiting for a connection...");
                // Enter the listening loop.
                while (true)
                {
                    // Perform a blocking call to accept requests.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine($"SERVER- Number of client connected: {++Count}");
                    Console.WriteLine(new string('*', 40));
                    //Create a thread to receive and send message
                    Thread thread = new Thread(new ParameterizedThreadStart(ProcessMessage));
                    thread.Start(client);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("SERVER- Exception: {0}", e.Message);
            }
            finally
            {
                server.Stop();
                Console.WriteLine("SERVER- Server stopped. Press any key to exit !");
            }
            Console.Read();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Lab 1: Server App");
            ExecuteServer("127.0.0.1", 8080);
        }
    }
}
