using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAppClient.UnitTests
{
    class DateValidatorTests
    {
        [Test]
        public void ValidateDate_PervectScenario_ShouldReturnTrue()
        {
            DateTime date1 = new DateTime(2020,11,1);
            DateTime date2 = new DateTime(2020, 11, 15);
            Assert.IsTrue(DateValidator.ValidateDate(date1, date2));
        }
        [Test]
        public void ValidateDate_BorrowStartDateIsBiggerThanDeadline_ShouldReturnFalse()
        {
            DateTime date1 = new DateTime(2020, 12, 1);
            DateTime date2 = new DateTime(2020, 11, 15);
            Assert.IsFalse(DateValidator.ValidateDate(date1, date2));
        }
    }
}
