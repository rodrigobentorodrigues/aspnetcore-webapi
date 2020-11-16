using Flunt.Notifications;
using Flunt.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPNET_Core.Models
{
    public class Category : Notifiable, IValidatable
    { 

        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public Category()
        {
            this.Products = new List<Product>();
        }

        public void Validate()
        {
            AddNotifications(new Contract().
                HasMaxLen(Title, 120, "Title", "O titulo deve conter até 120 caracteres").
                HasMinLen(Title, 3, "Title", "O titulo deve conter pelo menos 3 caracteres"));
        }

    }
}
