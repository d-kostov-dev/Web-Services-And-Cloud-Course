namespace Application.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// This model is created for testing purposes only. Ignore it!
    /// </summary>
    public class TestEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual User User { get; set; }
    }
}
