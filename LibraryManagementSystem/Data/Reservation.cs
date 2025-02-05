using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Data{

    public class Reservation{
        [Key]
        public int ReservationID {get;set;}
        public int BookID {get;set;}
        [ForeignKey("BookID")]
        public Book Book {get;set;} = null!;
        public int StudentID{get;set;}
        [ForeignKey("StudentID")]
        public Student Student {get;set;} = null!;
        public DateTime Registration {get;set;}
    }
}