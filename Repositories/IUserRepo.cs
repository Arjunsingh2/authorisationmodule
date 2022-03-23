using IPAuthorisation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPAuthorisation.Repositories
{
    interface IUserRepo
    {
        User GetUser(User user);
    }
}
