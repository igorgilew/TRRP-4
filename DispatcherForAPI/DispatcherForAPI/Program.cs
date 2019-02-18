using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatcherForAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Dispatcher dispatcher = Dispatcher.GetInstance();

            Console.WriteLine(dispatcher.GetWorkingServer());            
            Console.ReadLine();
        }
    }
}
