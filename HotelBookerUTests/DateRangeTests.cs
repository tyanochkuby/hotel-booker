using HotelBooker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookerUTests
{
    [TestFixture]
    public class DateRangeTests
    {
        [Test]
        public void Overlaps_ShouldReturnTrue_WhenRangesOverlap()
        {
            // Arrange
            var range1 = new DateRange(new DateTime(2023, 1, 1), new DateTime(2023, 1, 10));
            var range2 = new DateRange(new DateTime(2023, 1, 5), new DateTime(2023, 1, 15));

            // Act
            var result = range1.Overlaps(range2);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Overlaps_ShouldReturnFalse_WhenRangesDoNotOverlap()
        {
            // Arrange
            var range1 = new DateRange(new DateTime(2023, 1, 1), new DateTime(2023, 1, 10));
            var range2 = new DateRange(new DateTime(2023, 1, 11), new DateTime(2023, 1, 20));

            // Act
            var result = range1.Overlaps(range2);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Overlaps_ShouldReturnFalse_WhenRangesTouch1()
        {
            // Arrange
            var range1 = new DateRange(new DateTime(2023, 1, 1), new DateTime(2023, 1, 10));
            var range2 = new DateRange(new DateTime(2023, 1, 10), new DateTime(2023, 1, 20));

            // Act
            var result = range1.Overlaps(range2);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Overlaps_ShouldReturnFalse_WhenRangesTouch2()
        {
            // Arrange
            var range1 = new DateRange(new DateTime(2023, 1, 10), new DateTime(2023, 1, 20));
            var range2 = new DateRange(new DateTime(2023, 1, 1), new DateTime(2023, 1, 10));

            // Act
            var result = range1.Overlaps(range2);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Overlaps_ShouldReturnTrue_WhenOneRangeIsWithinAnother()
        {
            // Arrange
            var range1 = new DateRange(new DateTime(2023, 1, 1), new DateTime(2023, 1, 20));
            var range2 = new DateRange(new DateTime(2023, 1, 5), new DateTime(2023, 1, 10));

            // Act
            var result = range1.Overlaps(range2);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
