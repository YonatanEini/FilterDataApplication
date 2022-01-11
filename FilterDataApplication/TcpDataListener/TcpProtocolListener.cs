using FilterDataApplication.DecodedFrameModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FilterDataApplication.TcpDataListener
{
    /// <summary>
    /// Tcp Listenter Which Converting The Kafka Consumer Data To DecodedFrameDto Class Object And Updating The DB In Real-Time
    /// </summary>
    public class TcpProtocolListener
    {
        public int PortNumber { get; set; }
        public TcpListener Server { get; set; }
        public TcpProtocolListener(int portNo)
        {
            this.PortNumber = portNo;
        }
        public void ActivateServer()
        {
            try
            {
                Server = new TcpListener(IPAddress.Any, PortNumber);
                Server.Start();
                AcceptConnection();
                Console.WriteLine($"Activating Tcp Server on Port {PortNumber}");
            }
            catch (SocketException)
            {
                Console.WriteLine("Port Is Unavaliable");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Invalid Port Or Ip Address");
            }
        }
        public void AcceptConnection()
        {
            this.Server.BeginAcceptTcpClient(HandleConnection, this.Server);
        }
        public void HandleConnection(IAsyncResult result)
        {
            AcceptConnection();
            TcpClient client = Server.EndAcceptTcpClient(result);
            _ = Task.Factory.StartNew(() => ReadDataAsync(client));
        }
        public async Task ReadDataAsync(TcpClient client)
        {
            //connecting to mongodb to insert data into database
            MongoDbConnection dataBaseConnection = MongoDbConnection.GetInstance("FillterAppDataBase", "Collection");
            NetworkStream ns = client.GetStream();
            byte[] data = new byte[4];
            int bytes = ns.Read(data, 0, data.Length);
            while (bytes >= 0)
            {
                string client_data = Encoding.ASCII.GetString(data, 0, bytes);
                try
                {
                    int nextMsgLength = int.Parse(client_data);
                    // Date Received Is Message Byte Size
                    data = new byte[nextMsgLength];
                    bytes = ns.Read(data, 0, data.Length);
                }
                catch (IOException)
                {
                    bytes = 0;
                    Console.WriteLine("Client has disconnected the server");
                }
                catch (FormatException)
                {
                    // Date Received Is DecodedFrame In Json Format
                    Console.WriteLine(client_data);
                    DecodedFrameDto decodedFrameDto = JsonConvert.DeserializeObject<DecodedFrameDto>(client_data);
                    await dataBaseConnection.ClientMongoCollection.InsertOneAsync(decodedFrameDto);
                    Console.WriteLine(decodedFrameDto);
                    data = new byte[4];
                    bytes = ns.Read(data, 0, data.Length);
                }
            }
        }
    }
}
