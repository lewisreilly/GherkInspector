namespace GherkInspector.CLI
{
    using System;
    using GherkInspector.Parser;
    using GherkInspector.Parser.CodeInspector;

    internal class Program
    {
        /// <summary>
        /// GherkInspector.
        /// </summary>
        /// <param name="path">Path of the root directory where your feature files are located.</param>
        private static void Main(string path = null)
        {
            if (path == null)
            {
                Console.WriteLine("Please provide the path of the root directory where your feature files are located.");
                return;
            }

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
            Console.WriteLine();

            var featureFileReader = new FeatureFileReader();
            var overview = featureFileReader.Read(path);

            Console.WriteLine($"Total feature files: {overview.TotalFeatureCount}");
            Console.WriteLine($"Total scenarios:     {overview.TotalScenarioCount}");
            Console.WriteLine();

            string currentPath = string.Empty;
            foreach (var feature in overview.Features)
            {
                if (currentPath != feature.Path)
                {
                    currentPath = feature.Path;
                    Console.WriteLine(currentPath.Replace(path, string.Empty));
                    Console.WriteLine();
                }

                Console.WriteLine($"    {feature.Name}.feature");

                foreach (var scenario in feature.Scenarios)
                {
                    Console.WriteLine($"        Scenario: {scenario.Name}");

                    var inspector = new Inspector();
                    inspector.InspectScenario(scenario);

                    if (inspector.HasWarnings)
                    {
                        foreach (var warning in inspector.Warnings)
                        {
                            Console.WriteLine($"            > GKN100{warning.Error}");
                        }
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
