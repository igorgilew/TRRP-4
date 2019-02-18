using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServersForApiConfigurator
{
    class Server
    {
        private IPAddress address = IPAddress.Parse("127.0.0.1");
        private int port;
        private TcpListener server = null;
        private TcpClient client = null;
        private Form1 parent;

        public Server(int port, Form1 parent)
        {
            this.port = port;
            this.parent = parent;
            server = new TcpListener(address, port);
        }
        public async void StartServer()
        {         
            try
            {
                server.Start();
                Console.WriteLine($"Server {address.ToString()}:{port} started. Waiting connections... ");
                parent.rtbLog.AppendText($"{DateTime.Now.ToShortTimeString()}: Server {address.ToString()}:{port} started. Waiting connections... \n");
                while (true)
                {
                    //Console.WriteLine($"Server {address.ToString()}:{port} started. Waiting connections... ");
                    // получаем входящее подключение
                    client = await server.AcceptTcpClientAsync();
                    
                    if(client != null)
                    {
                        Console.WriteLine("Подключен клиент. Выполнение запроса...");
                        
                        //выполнять запрос к открытым данным
                        ///Тут должен быть метод для получения открытых данных и возврата на клиент, на диспетчер нам не нужно 
                        ///ничего отправлять там важно только чтоб подключение устанавливалось к серверу (сервер работает)
                        /*
                        // получаем сетевой поток для чтения и записи
                        NetworkStream stream = client.GetStream();

                        // сообщение для отправки клиенту
                        string response = "Привет мир";
                        // преобразуем сообщение в массив байтов
                        byte[] data = Encoding.UTF8.GetBytes(response);

                        // отправка сообщения
                        stream.Write(data, 0, data.Length);
                        Console.WriteLine("Отправлено сообщение: {0}", response);
                        // закрываем поток
                        stream.Close();
                        */
                        // закрываем подключение
                        client.Close();
                    }
                    Thread.Sleep(3000);
                    

                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }
        public void StopServer()
        {            
            server.Stop();
            Console.WriteLine($"Server {address.ToString()}:{port} was stopped.");
            parent.rtbLog.AppendText($"{DateTime.Now.ToShortTimeString()}: Server {address.ToString()}:{port} was stopped.\n");
        }
    }
}
