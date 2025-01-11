using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lab1
{
    internal class Program
    {
        static void ConnectServer(String server, int port)
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.SetMinimumLevel(LogLevel.Information)); // Set to Information level
            ILogger logger = factory.CreateLogger("Program");
            string message, responseData;
            int bytes;
            try
            {
                // Create a TcpClient
                TcpClient client = new TcpClient(server, port);
                
                NetworkStream stream = null;
                while (true)
                {
                    Console.Write("CLIENT- Input message <press Enter to exit>:");
                    message = Console.ReadLine();
                    if (message == string.Empty)
                    {
                        break;
                    }
                    // Translate the passed message into ASCII and store it as a byte array.
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes($"{message}");
                    // Get a client stream for reading and writing.
                    stream = client.GetStream();
                    // Send the message to the connected TcpServer.
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("CLIENT- Sent data: {0}", message);
                    // Receive the TcpServer response.
                    // Use buffer to store the response bytes.
                    data = new Byte[256];
                    // Read the first batch of the TcpServer response bytes.
                    bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("CLIENT- Received data: {0}", responseData);
                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("CLIENT- Exception: {0}", e.Message);
                //end ConnectServer
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Lab 1: Client App");
            ConnectServer("127.0.0.1", 8080);
        }
    }
}
