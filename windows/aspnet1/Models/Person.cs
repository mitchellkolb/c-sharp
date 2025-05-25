using System.ComponentModel.DataAnnotations;

namespace TallyBoard.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public int TallyAmount { get; set; }
    }
}
