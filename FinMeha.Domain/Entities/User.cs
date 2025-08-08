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
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; } // Atenção à nota de segurança abaixo!

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User(int id, string name, string lastName, string email, string password, DateTime createdAt)
        {

            if(id == 0)
            {
                throw new InvalidOperationException("Esse campo não pode ser menor ou igual a zero");
            }

            this.Id = id;

            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(lastName))
            {
                throw new InvalidOperationException("Este campo deve ser preenchido");
            }

            this.Name = name;
            this.LastName = lastName;

            if (string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException("Campo deve ser preenchido!");
            }
            this.Password = password;
            this.CreatedAt = createdAt;

            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
            CreatedAt = createdAt;
        }
    }
}
