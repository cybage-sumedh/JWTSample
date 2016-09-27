using System.ComponentModel.DataAnnotations;

namespace JsonWebTokensWebApi.Models
{
    public class AudienceModel
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}