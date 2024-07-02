using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities.ChatHub;
using School.Repository.Interfaces.ChatHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Repositories.ChatHub
{
    public class ActiveUserRepository:GenericRepository<ActiveUserConnection> , IActiveUserRepository
    {
        public ActiveUserRepository(SchoolDbContext context) : base(context) { }
        public async Task AddActiveUser(string UserId, string ConnectionId)
        {
            var existing_user = await _context.ActiveUserConnections.FirstOrDefaultAsync(c => c.UserId == UserId);
            if (existing_user == null)
            {
                _context.ActiveUserConnections.Add(new ActiveUserConnection()
                {
                    ConnectionId = ConnectionId,
                    UserId = UserId
                });
            }
            else
            {
                existing_user.ConnectionId = ConnectionId;
                _context.ActiveUserConnections.Update(existing_user);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<ActiveUserConnection> GetActiveUserByUserId(string UserId)
        {
            return await _context.ActiveUserConnections.FirstOrDefaultAsync(c => c.UserId == UserId);
        }
        public async Task RemoveActiveUser(string ConnectionId)
        {
            ActiveUserConnection activeUser = await _context.ActiveUserConnections.FirstOrDefaultAsync(c => c.ConnectionId == ConnectionId);
            if (activeUser == null)
                return;
            _context.ActiveUserConnections.Remove(activeUser);
            await _context.SaveChangesAsync();
        }
    }
}
