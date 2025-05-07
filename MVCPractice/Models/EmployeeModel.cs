using System.ComponentModel.DataAnnotations;

namespace MVCPractice.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="FirstName Required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "LastName Required")]
        public string? LastName { get; set; }
        public int? Age { get; set; }
        [Required(ErrorMessage = "DOB Required")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string? State { get; set; }

        public string? Gender { get; set; }

    }
}
