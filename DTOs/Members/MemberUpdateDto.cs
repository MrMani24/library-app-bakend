using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace library_app_bakend.DTOs.Members
{
    public class MemberUpdateDto
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Wealth { get; set; }
    }
}