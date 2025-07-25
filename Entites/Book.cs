using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library_app_bakend.Entites.Base;

namespace library_app_bakend.Entites
{
    public class Book : Thing
    {
        public required string Title { get; set; }
        public string? Writer { get; set; }
        public string? Publisher { get; set; }
        public double Price { get; set; }
    }
}