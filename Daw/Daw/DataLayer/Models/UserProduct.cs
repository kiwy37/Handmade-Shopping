using System.ComponentModel.DataAnnotations;

namespace Daw.DataLayer.Models
{
    public class UserProduct : BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
