using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACN.Core.Entity
{
    public class User
    {
        public string Name { get; set; }    
        public string Email { get; set; }
        public string Password { get; set; }

        public int IsAdmin { get; set; }
        
        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }


    }
}
