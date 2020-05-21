using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryAppClient.Modells
{
    public class BorrowedBook
    {
        public long Id { get; set; }
        public long ReaderNumber { get; set; }
        public string ReaderName { get; set; }
        public long BookNumber { get; set; }
        public string BookName { get; set; }
        public DateTime DateOfBorrow { get; set; }
        public DateTime Deadline { get; set; }


        public override string ToString()
        {
            return $"{Id} Kikölcsönözte: {BookName} című könyvet {ReaderNumber} {ReaderName} ekkor {DateOfBorrow} vissza kell hoznia eddig {Deadline}";
        }
    }
}
