using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Domain.Identity;

namespace ProjectTracker.Domain.Interface
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(AppUser user);
    }
}
