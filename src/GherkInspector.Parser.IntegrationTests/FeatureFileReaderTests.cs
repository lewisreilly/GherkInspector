namespace GherkInspector.Parser.IntegrationTests
{
    using System.IO;
    using System.Reflection;
    using NUnit.Framework;

    [TestFixture]
    public class FeatureFileReaderTests
    {
        [Test]
        [Explicit]
        public void Read_DirectoryContainingFeatureFiles_FindsThem()
        {
            var featureFileReader = new FeatureFileReader();
            var rootDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var results = featureFileReader.Read($"{rootDirectory}/Features");

            Assert.That(results.TotalFeatureCount, Is.EqualTo(3));
            Assert.That(results.TotalScenarioCount, Is.EqualTo(10));
        }
    }
}
