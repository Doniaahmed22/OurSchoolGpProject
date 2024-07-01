    using School.Data.Context;
using School.Data.Entities;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Repositories
{
    public class ChatRepository: GenericRepository<Message>,IChatRepository
    {
        public ChatRepository(SchoolDbContext context) : base(context) { }
    }
}
