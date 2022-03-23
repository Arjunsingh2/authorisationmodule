using IPAuthorisation.Models;
using IPAuthorisation.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPAuthorisation.Repositories
{
    public class UserRepo:IUserRepo
    {
        private readonly UserContext _ctx;
        public UserRepo(UserContext ctx)
        {
            _ctx = ctx;
            if (_ctx.Users.Any())
                return;
            List<User> users = new List<User>()
            {
                new User("geetha","geetha123"),
                new User("jyothsana","joe123"),
                new User("test","test123")
            };
            foreach(var u in users)
            {
                _ctx.Users.Add(u);
            }
            _ctx.SaveChanges();

        }
        public User GetUser(User user)
        {
            User u=_ctx.Users.Where(p => p.Username == user.Username && p.Password == user.Password).FirstOrDefault() ;
            if (u != null)
                return u;
            return null;
        }

    }
}
