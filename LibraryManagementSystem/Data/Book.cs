using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Data{

    public class Book{
        [Key]
        public int BookID {get;set;}
        public string? BookName {get;set;}
        public string? Author {get;set;}
        public string BookInfo {get{return this.BookName + " (" + this.Author + ")";}}
        [Range(1000, 9999)]
        public int? PublicationYear {get;set;}
        public string? Category {get;set;}
        public DateTime Registration {get;set;}
        public bool IsActive { get; set; } = true;
        public ICollection<Reservation> Reservations {get;set;} = new List<Reservation>();

    }
}