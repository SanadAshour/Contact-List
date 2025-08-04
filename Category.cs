using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact
{
    class Category
    {
        public int id { get; set; }
        public string? Name { get; set; }
        public List<MyContacts> ConList { get; set; } = new List<MyContacts>();
    }
}
