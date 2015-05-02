
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace OnlineHotelReservationSystem.Areas.Customer.Models
{
    public class ReservationMetaData
    {
             
            
    }
    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [MinLength(4, ErrorMessage = "At least 4 characters required!")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [DisplayName("FullName :")]

        [Required(ErrorMessage = "Fullname is Required")]
        public string Fullname { get; set; }

        //[Remote("CheckIsEmailExist", "CustomerReservation", "There is already A Booking for this Email.")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        [DisplayName("EmailId : ")]
        [Required(ErrorMessage = "Email is Required")]
      
        public string Email { get; set; }
        [DisplayName("PhoneNumber : ")]
        [Required(ErrorMessage = "Phonenumber is Required")]

        public string Phonenumber { get; set; }
        [DisplayName("Country : ")]
        public string Country { get; set; }
        [DisplayName("Standard : ")]
        public int? Standard { get; set; }
        [DisplayName("Suit : ")]
        public int? Suit { get; set; }
        [DisplayName("Deluxi : ")]
        public int? Deluxi { get; set; }
        [DisplayName("Delux : ")]
        public int? Delux { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Indate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Outdate { get; set; }
    }

    public class ReservationDbContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
    }
}