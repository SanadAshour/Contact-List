using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact
{
    internal class MyContacts
    {
        public int id { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public int? PhoneNumber { get; set; }
        public string? email { get; set; }
        public string? gender { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
