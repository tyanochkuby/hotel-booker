using HotelBooker.Helpers;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelBookerUTests
{
    
    [TestFixture]
    public class DateTimeJsonConverterTests
    {
        private DateTimeJsonConverter _converter;
        private JsonSerializerOptions _options;

        [SetUp]
        public void Setup()
        {
            _converter = new DateTimeJsonConverter();
            _options = new JsonSerializerOptions
            {
                Converters = { _converter }
            };
        }

        [Test]
        public void Read_ShouldParseDateCorrectly()
        {
            // Arrange
            string json = "\"20230101\"";
            var reader = new Utf8JsonReader(System.Text.Encoding.UTF8.GetBytes(json));

            // Act
            reader.Read();
            DateTime result = _converter.Read(ref reader, typeof(DateTime), _options);

            // Assert
            Assert.That(result, Is.EqualTo(new DateTime(2023, 1, 1)));
        }

        [Test]
        public void Write_ShouldFormatDateCorrectly()
        {
            // Arrange
            DateTime date = new DateTime(2023, 1, 1);
            var buffer = new ArrayBufferWriter<byte>();
            var writer = new Utf8JsonWriter(buffer);

            // Act
            _converter.Write(writer, date, _options);S
            writer.Flush();
            string json = System.Text.Encoding.UTF8.GetString(buffer.WrittenMemory.ToArray());

            // Assert
            Assert.That(json, Is.EqualTo("\"20230101\"")S);
        }
    }
}


