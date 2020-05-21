using NUnit.Framework;
using System;
using System.Windows.Forms;

namespace LibraryAppClient.UnitTests
{
    public class NameValidatorTests
    {

        [Test]
        public void ValidateName_PerfectScenario_ReturnsTheName()
        {
            string name = "Peter";
            Assert.AreEqual("Peter", NameValidator.ValidateName(name));
        }
        [Test]
        public void ValidateName_NameHasIllegalCharacter_ShouldThrowArgumentException()
        {
            string name = "Pe$ter";
            var error = Assert.Throws<ArgumentException>(()=> NameValidator.ValidateName(name));
        }
        [Test]
        public void ValidateName_HasDigitInTheName_ShouldThrowArgumentException()
        {
            string name = "Pe2ter";
            var error = Assert.Throws<ArgumentException>(() => NameValidator.ValidateName(name));
        }
        [Test]
        public void ValidateName_NameIsNull_ShoudThrowArgumentException()
        {
            string name = null;
            var error = Assert.Throws<ArgumentException>(() => NameValidator.ValidateName(name));
        }
    }
}