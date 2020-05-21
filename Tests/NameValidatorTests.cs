using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryAppClient.UnitTests
{
    [TestClass]
    public class NameValidatorTests
    {
        [TestMethod]
        public void NameValidator_EverythingIsPerfect_ReturnsName()
        {
            var validaror = new NameValidator();
            var result = NameValidator.ValidateName();
        }
    }
}
