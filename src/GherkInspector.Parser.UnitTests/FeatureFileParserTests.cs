using NUnit.Framework;

namespace GherkInspector.Parser.UnitTests
{
    public class FeatureFileParserTests
    {
        private readonly FeatureFileParser FeatureFileParser;

        public FeatureFileParserTests()
        {
            FeatureFileParser = new FeatureFileParser();
        }

        [Test]
        public void Parsing_a_feature_file_creates_an_in_memory_model_of_its_structure()
        {
            var result = FeatureFileParser.Parse(@"
Feature: Example
");

            Assert.That(result.Name, Is.EqualTo("Example"));
        }
    }
}