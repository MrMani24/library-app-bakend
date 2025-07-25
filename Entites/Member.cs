using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library_app_bakend.Entites.Base;
using library_app_bakend.Enums;

namespace library_app_bakend.Entites
{
    public class Member : Thing
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public Gender Gender { get; set; }
    }
}