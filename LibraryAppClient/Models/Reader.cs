using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryAppClient.Modells
{
    public class Reader
    {
        public long Id { get; set; }
        private string _firstName;
        private string _lastName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = NameValidator.ValidateName(value);
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = NameValidator.ValidateName(value);
            }
        }

        public Reader() { }
        public Reader(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }

        public override string ToString()
        {
            return $"{Id} {FirstName} {LastName}";
        }
    }

}
