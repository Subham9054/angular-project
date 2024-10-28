
using Login.Model.Entities.LoginEntity;
using Login.Repository.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Repository.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        public Task<Users> login(Users user);
       // public Task<LoginEntity> login(LoginEntity user);
       // Task<List<LoginEntity>> GetChecklogindetails(LoginEntity TBL);
    }
}
