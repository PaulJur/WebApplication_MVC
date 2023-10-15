using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Category
    {
        [Key]//always makes this the primary key
        public int CategoryId { get; set; }
        [Required]//a required data annotation in a SQL code
        [MaxLength(30)]//Limits the category name to 30 characters
        [DisplayName("Category Name")] //These are data annotations for UI based that will display the typed name for the variables.
        public required string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage = "Display Order must be between 1-100")]//Minimum display order to maximum amount
        public int DisplayOrder { get; set; }
    }
}
