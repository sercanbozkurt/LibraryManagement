using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Data{

    public class Student{
        
        [Key]
        public int StudentID {get;set;}
        public string? StudentName {get;set;}
        public string? StudentSurname {get;set;}
        public string AdSoyad{get{return this.StudentName + " " + this.StudentSurname;}}
        public string? Email {get;set;}
        public string? Phone {get;set;}
        public DateTime Registration {get;set;}
        public bool IsActive { get; set; } = true;

        public ICollection<Reservation> Reservations {get;set;} = new List<Reservation>();



    }
}