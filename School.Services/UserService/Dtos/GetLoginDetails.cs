using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.UserService.Dtos
{
    public class GetLoginDetails
    {
        public int id { get; set; }
        public string userId { get; set; }
        public string role { get; set; }
        public string token { get; set; }
    }
}
