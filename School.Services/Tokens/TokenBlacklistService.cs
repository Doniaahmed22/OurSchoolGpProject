using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Tokens
{
    public class TokenBlacklistService
    {
        private readonly HashSet<string> _blacklist = new();

        public void BlacklistToken(string token)
        {
            _blacklist.Add(token);
        }

        public bool IsTokenBlacklisted(string token)
        {
            return _blacklist.Contains(token);
        }
    }
}
