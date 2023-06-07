using System.ComponentModel.DataAnnotations;

namespace ShoppyApp.Model
{
    public class Category
    {
        // Class communicates with DB to categorize items
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Display Order")]
        [Range(1, 100, ErrorMessage = "Please Enter a number between 1 and 100")]
        public int DisplayOrder { get; set; }



    }
}
