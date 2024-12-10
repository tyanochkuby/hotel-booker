using System;
using System.Collections.Generic;
using System.IO;
using HotelBooker.Helpers;
using HotelBooker.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HotelBookerUTests
{
    [TestFixture]
    public class ConfigurationManagerTests
    {
        private string _validHotelsFilePath;
        private string _validBookingsFilePath;
        private string _invalidJsonFilePath;

        [SetUp]
        public void SetUp()
        {
            _validHotelsFilePath = Path.GetTempFileName();
            _validBookingsFilePath = Path.GetTempFileName();
            _invalidJsonFilePath = Path.GetTempFileName();

            File.WriteAllText(_validHotelsFilePath, @"
            [
                {
                    ""id"": ""H1"",
                    ""name"": ""Hotel California"",
                    ""roomTypes"": [
                        {
                            ""code"": ""SGL"",
                            ""description"": ""Single Room""
                        },
                        {
                            ""code"": ""DBL"",
                            ""description"": ""Double Room""
                        }
                    ],
                    ""rooms"": [
                        { ""roomType"": ""SGL"", ""roomId"": ""101"" },
                        { ""roomType"": ""SGL"", ""roomId"": ""102"" },
                        { ""roomType"": ""DBL"", ""roomId"": ""201"" },
                        { ""roomType"": ""DBL"", ""roomId"": ""202"" }
                    ]
                }
            ]");

            File.WriteAllText(_validBookingsFilePath, @"
            [
                {
                    ""hotelId"": ""H1"",
                    ""arrival"": ""20240901"",
                    ""departure"": ""20240903"",
                    ""roomType"": ""DBL"",
                    ""roomRate"": ""Prepaid""
                },
                {
                    ""hotelId"": ""H1"",
                    ""arrival"": ""20240902"",
                    ""departure"": ""20240905"",
                    ""roomType"": ""SGL"",
                    ""roomRate"": ""Standard""
                }
            ]");

            File.WriteAllText(_invalidJsonFilePath, "Invalid JSON content");
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_validHotelsFilePath);
            File.Delete(_validBookingsFilePath);
            File.Delete(_invalidJsonFilePath);
        }

        [Test]
        public void GetConfig_ShouldReturnValidConfiguration()
        {
            // Arrange
            string[] args = { "--hotels", _validHotelsFilePath, "--bookings", _validBookingsFilePath };

            // Act
            var config = HotelBookerConfigurationManager.GetConfig(args);

            // Assert
            Assert.That(config["Hotels"], Is.EqualTo(_validHotelsFilePath));
            Assert.That(config["Bookings"], Is.EqualTo(_validBookingsFilePath));
        }

        [Test]
        public void ValidateConfig_ShouldReturnTrueForValidConfig()
        {
            // Arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Hotels", _validHotelsFilePath },
                    { "Bookings", _validBookingsFilePath }
                })
                .Build();

            // Act
            var result = HotelBookerConfigurationManager.ValidateConfig(config);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ValidateConfig_ShouldReturnFalseForMissingConfigValues()
        {
            // Arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>())
                .Build();

            // Act
            var result = HotelBookerConfigurationManager.ValidateConfig(config);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ValidateConfig_ShouldReturnFalseForNonExistentFiles()
        {
            // Arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Hotels", "nonexistent_hotels.json" },
                    { "Bookings", "nonexistent_bookings.json" }
                })
                .Build();

            // Act
            var result = HotelBookerConfigurationManager.ValidateConfig(config);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void TryLoadJson_ShouldReturnTrueForValidJsonFile()
        {
            // Act
            var result = HotelBookerConfigurationManager.TryLoadJson(_validHotelsFilePath, out List<Hotel> hotels);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(hotels, Is.Not.Null);
            Assert.That(hotels.Count, Is.EqualTo(1));
            Assert.That(hotels[0].Id, Is.EqualTo("H1"));
        }

        [Test]
        public void TryLoadJson_ShouldReturnFalseForInvalidJsonFile()
        {
            // Act
            var result = HotelBookerConfigurationManager.TryLoadJson(_invalidJsonFilePath, out List<Hotel> hotels);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(hotels, Is.Empty);
        }
    }
}


