using FinMeha.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinMeha.Domain.Interfaces
{
    public interface ITokenService
    {
        //Recebe o usuário validado e retorna o token como string.
        //A geração do token geralmente é síncrona
        string GenerateToken(User user);
    }
}
