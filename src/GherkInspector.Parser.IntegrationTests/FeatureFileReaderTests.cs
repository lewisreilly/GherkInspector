using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace GherkInspector.Parser.IntegrationTests
{
    [TestFixture]
    public class FeatureFileReaderTests
    {
        [Test]
        public void Read_DirectoryContainingFeatureFiles_FindsThem()
        {
            var featureFileReader = new FeatureFileReader();
            var rootDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var results = featureFileReader.Read($"{rootDirectory}/Features");

            Assert.That(results.TotalFeatureCount, Is.EqualTo(4));
            Assert.That(results.TotalScenarioCount, Is.EqualTo(9));
        }
    }
}
