using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace library_app_bakend.DTOs.Members
{
    public class MemberUpdateUnaAndPssDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}