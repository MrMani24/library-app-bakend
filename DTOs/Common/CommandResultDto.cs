using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace library_app_bakend.DTOs.Common
{
    public class CommandResultDto
    {
        public Boolean Successfull { get; set; }
        public string? Massage { get; set; }
    }
}