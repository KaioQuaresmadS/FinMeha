using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FinMeha.Domain.Entities;

namespace FinMeha.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
