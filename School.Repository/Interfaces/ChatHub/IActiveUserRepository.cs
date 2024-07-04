using School.Data.Entities.ChatHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces.ChatHub
{
    public interface IActiveUserRepository:IGenericRepository<ActiveUserConnection>
    {
        Task AddActiveUser(string UserId, string ConnectionId);
        Task RemoveActiveUser(string UserId);
        Task<ActiveUserConnection> GetActiveUserByUserId(string UserId);

    }
}
