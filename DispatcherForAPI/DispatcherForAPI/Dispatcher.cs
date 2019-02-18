using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

//using thread safety realization of singleton pattern
namespace DispatcherForAPI
{
    public class Dispatcher
    {
        private static readonly Dispatcher instance = new Dispatcher();
        
        //address, port, status, countClients  
        private List<Tuple<string, int, bool, int>> addressAndPortContainer = new List<Tuple<string, int, bool, int>>();
        private string pathToServers = $"{Environment.CurrentDirectory}\\servers.txt";
        //tcpClient.Connect ("www.microsoft.com", 80);
        
        private Dispatcher()
        {
            //тут вызывать метод чекающий сервера
            ReadAdresses();                        
        }
        public static Dispatcher GetInstance()
        {
            return instance;
        }
        

        private void ReadAdresses()
        {
            addressAndPortContainer.Clear();
            using (StreamReader sr = new StreamReader(pathToServers, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Replace(" ", string.Empty);
                    var parsedLine = line.Split(':');                    
                    addressAndPortContainer.Add(Tuple.Create(parsedLine[0], int.Parse(parsedLine[1]), false, 0));
                }
            }
        }

        private int currentMinCountClients = 0;
        /// <summary>
        /// In this func we check connection with servers and find the min value of potential clients of some server
        
        /// </summary>
        private void CheckServersStatus()
        {
            int minCountClients = int.MaxValue;
            for (int i=addressAndPortContainer.Count-1; i>=0; i--)
            {
                var tpl = addressAndPortContainer[i];
                try
                {
                    using (var tcpClient = new TcpClient())
                    {
                        tcpClient.Connect(tpl.Item1, tpl.Item2);
                        addressAndPortContainer.Add(Tuple.Create(tpl.Item1, tpl.Item2, true, tpl.Item4));                        
                    }
                    
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    //тут не зануляю количество клиентов, потому что клиенты будут вставать в очередь 
                    //и могут не успеть обслужиться поэтому нужно учитывать историю
                    addressAndPortContainer.Add(Tuple.Create(tpl.Item1, tpl.Item2, false, tpl.Item4));
                }
                if (tpl.Item4 < minCountClients) minCountClients = tpl.Item4;
                addressAndPortContainer.Remove(tpl);
            }
            currentMinCountClients = minCountClients;
            foreach (var tpl in addressAndPortContainer)
            {
                Console.WriteLine($"{tpl.Item1}:{tpl.Item2} Status: {tpl.Item3} Count of clients: {tpl.Item4}");
            }
        }
        
        public string GetWorkingServer()
        {
            string serverAddress = string.Empty;
            CheckServersStatus();
            var properTuple = addressAndPortContainer.Find(x => (x.Item3 && x.Item4 == currentMinCountClients));
            serverAddress = $"{properTuple.Item1}:{properTuple.Item2}";
            addressAndPortContainer.Remove(properTuple);
            addressAndPortContainer.Add(Tuple.Create(properTuple.Item1, properTuple.Item2, properTuple.Item3, properTuple.Item4 + 1));
            return serverAddress;
        }

    }
}
