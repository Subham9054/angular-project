using Dapper;
using GMS.Repository.BaseRepository;
using Login.Model.Entities.LoginEntity;
using Login.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMS.Repository.Factory;

namespace Login.Repository.Repositories.Repository
{
    public class LoginRepository : db_PHED_CGRCRepositoryBase, ILoginRepository
    {
        public LoginRepository(Idb_PHED_CGRCConnectionFactory db_PHED_CGRCConnectionFactory) : base(db_PHED_CGRCConnectionFactory)
        {

        }
        public async Task<Users> login(Users user)
        {
            try
            {
                //var password = Md5Encryption.MD5Encryption(user.vchPassWord);
                var parameters = new DynamicParameters();
                parameters.Add("@Action", "LI");
                parameters.Add("@username", user.vchUserName);
                parameters.Add("@password", user.vchPassWord);
                var users = await Connection.QueryFirstOrDefaultAsync<Users>("Login_Registration", parameters, commandType: CommandType.StoredProcedure);
                return users;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public async Task<LoginEntity> login(LoginEntity user)
        //{
        //    try
        //    {
        //        //var password = Md5Encryption.MD5Encryption(user.vchPassWord);
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@Action", "LI");
        //        parameters.Add("@username", user.vchUserName);
        //        parameters.Add("@password", user.vchPassWord);
        //        LoginEntity users = await Connection.QueryFirstOrDefaultAsync<LoginEntity>("USP_Registration", parameters, commandType: CommandType.StoredProcedure);
        //        return users;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

      
    }
}
