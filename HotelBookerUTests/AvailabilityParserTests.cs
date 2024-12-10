using System;
using HotelBooker.Helpers;
using HotelBooker.Interfaces;
using HotelBooker.Models;
using NUnit.Framework;

namespace HotelBookerUTests
{
    [TestFixture]
    public class AvailabilityParserTests
    {
        private IAvailabilityParser _parser;

        [SetUp]
        public void SetUp()
        {
            _parser = new AvailabilityParser();
        }

        [Test]
        public void Parse_ValidInputWithStartAndEndDate_ShouldReturnCorrectAvailability()
        {
            // Arrange
            string input = "Availability(H1, 20240901-20240903, DBL)";

            // Act
            var result = _parser.Parse(input);

            // Assert
            Assert.That(result.HotelId, Is.EqualTo("H1"));
            Assert.That(result.RoomType, Is.EqualTo("DBL"));
            Assert.That(result.DateRange.Start, Is.EqualTo(new DateTime(2024, 9, 1)));
            Assert.That(result.DateRange.End, Is.EqualTo(new DateTime(2024, 9, 3)));
        }

        [Test]
        public void Parse_ValidInputWithOnlyStartDate_ShouldReturnCorrectAvailability()
        {
            // Arrange
            string input = "Availability(H1, 20240901, DBL)";

            // Act
            var result = _parser.Parse(input);

            // Assert
            Assert.That(result.HotelId, Is.EqualTo("H1"));
            Assert.That(result.RoomType, Is.EqualTo("DBL"));
            Assert.That(result.DateRange.Start, Is.EqualTo(new DateTime(2024, 9, 1)));
            Assert.That(result.DateRange.End, Is.EqualTo(new DateTime(2024, 9, 2)));
        }

        [Test]
        public void Parse_InvalidInputFormat_ShouldThrowFormatException()
        {
            // Arrange
            string input = "Is there any DBL room available for today?";

            // Act & Assert
            var ex = Assert.Throws<FormatException>(() => _parser.Parse(input));
            Assert.That(ex.Message, Is.EqualTo("Input string is not in the expected format."));
        }

        [Test]
        public void Parse_StartDateAfterEndDate_ShouldThrowFormatException()
        {
            // Arrange
            string input = "Availability(H1, 20240903-20240901, DBL)";

            // Act & Assert
            var ex = Assert.Throws<FormatException>(() => _parser.Parse(input));
        }
    }
}

