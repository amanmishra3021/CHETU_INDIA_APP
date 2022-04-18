using System;
using System.Collections.Generic;

#nullable disable

namespace CHETU_INDIA_APP.DB_Context
{
    public partial class ChetuEmplyoyee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Designation { get; set; }
        public long Mobile { get; set; }
    }
}
