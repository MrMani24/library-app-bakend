using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library_app_bakend.Enums;

namespace library_app_bakend.DTOs.Members
{
    public class MemberAddDto
    {
        public Gender Gender { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Wealth { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}