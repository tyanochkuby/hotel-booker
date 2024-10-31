using HotelBooker.Helpers;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;

namespace HotelBookerUTests
{
    [TestFixture]
    public class DateTimeJsonConverterTests
    {
        private DateTimeJsonConverter _converter;
        private JsonSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _converter = new DateTimeJsonConverter();
            _serializer = new JsonSerializer();
            _serializer.Converters.Add(_converter);
        }

        [Test]
        public void Read_ShouldParseDateCorrectly()
        {
            // Arrange
            string json = "\"20230101\"";
            var reader = new JsonTextReader(new StringReader(json));

            // Act
            var result = _serializer.Deserialize<DateTime>(reader);

            // Assert
            Assert.That(result, Is.EqualTo(new DateTime(2023, 1, 1)));
        }

        [Test]
        public void Write_ShouldFormatDateCorrectly()
        {
            // Arrange
            DateTime date = new DateTime(2023, 1, 1);
            var stringWriter = new StringWriter();
            var writer = new JsonTextWriter(stringWriter);

            // Act
            _serializer.Serialize(writer, date);
            writer.Flush();
            string json = stringWriter.ToString();

            // Assert
            Assert.That(json, Is.EqualTo("\"20230101\""));
        }
    }
}
