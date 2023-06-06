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
        public int DisplayOrder { get; set; }



    }
}
