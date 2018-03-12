using System.ComponentModel.DataAnnotations;

namespace DotNetCoreFood.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Restaurant Name")]
        [MaxLength(80)]
        public string Name { get; set; }

        public CuisineType Cuisine { get; set; }
    }
}
