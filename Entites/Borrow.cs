using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library_app_bakend.Entites.Base;

namespace library_app_bakend.Entites
{
    public class Borrow : PersonThing
    {
        public required Book Book { get; set; }
        public DateTime BorrwTime { get; set; }
        public DateTime? ReturnTime { get; set; }
    }
}