using System;

namespace GherkInspector.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            // http://www.patorjk.com/software/taag/#p=display&f=Graffiti&t=Type%20Something%20
            var big = @"
   _____ _               _    _____                           _             
  / ____| |             | |  |_   _|                         | |            
 | |  __| |__   ___ _ __| | __ | |  _ __  ___ _ __   ___  ___| |_ ___  _ __ 
 | | |_ | '_ \ / _ \ '__| |/ / | | | '_ \/ __| '_ \ / _ \/ __| __/ _ \| '__|
 | |__| | | | |  __/ |  |   < _| |_| | | \__ \ |_) |  __/ (__| || (_) | |   
  \_____|_| |_|\___|_|  |_|\_\_____|_| |_|___/ .__/ \___|\___|\__\___/|_|   
                                             | |                            
                                             |_|                            
";
            Console.WriteLine(big);

            Console.ReadKey();
        }
    }
}
