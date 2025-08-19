using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinMeha.Domain.Entities
{
    public class User
    {
        // Chave Primária para identificar o usuário de forma única no banco.
        public Guid Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; private set; } // Atenção à nota de segurança abaixo!

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        private User(Guid id, string fristName, string lastName, string email, string passwordHash)
        {


            Id = id;
            FirstName = fristName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
        }

        public static User Create(string fristName, string lastName, string email, string passwordHash)
        {
            //Aqui poderia existir validações de domínio, como garantir que o email não é nulo
            return new User(Guid.NewGuid(), fristName, lastName, email, passwordHash);

            //if(email.All(char.IsLetterOrDigit) || email.Contains("@") || email.Contains("."))
            //{
            //    throw new ArgumentException("Email inválido.");
            //}
        }
    }
}
