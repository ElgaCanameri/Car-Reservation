using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class AppUser : IdentityUser<int> { }
    public class AppRole : IdentityRole<int> { }
}
