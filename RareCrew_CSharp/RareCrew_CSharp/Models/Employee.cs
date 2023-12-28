using Microsoft.AspNetCore.Mvc;

namespace RareCrew_CSharp.Models
{
    public class Employee : IEntity
    {
        public string Id { get; set; }
        public string EmployeeName { get; set; }
        public DateTime StarTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public string EntryNotes { get; set; }
        public DateTime? DeletedOn { get; set; }
        public double TotalTimeWorked { get; set; }
    }
}
