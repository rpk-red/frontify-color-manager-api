using System.ComponentModel.DataAnnotations;

namespace ColorManager.Api.DTO
{
    public class ColorDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(7)]
        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "The field HexCode must match the 6 - character hex format value")]
        public string HexCode { get; set; }
    }
}
