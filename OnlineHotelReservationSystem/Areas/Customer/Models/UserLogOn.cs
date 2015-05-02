using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace OnlineHotelReservationSystem.Areas.Customer.Models
{
     [Table("Customer")]
    public class UserLogOn
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

    }

     public class UserLoginDbContext : DbContext
     {
         public DbSet<UserLogOn> UserLogOns { get; set; }
     }
}