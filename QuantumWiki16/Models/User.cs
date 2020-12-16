using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantumWiki16.Models
{
    [Table("Users")]
    public class User
    {
        //  F i e l d s   a n d   p r o p e r t i e s 

        public int Id { get; set; }

        [EmailAddress]
        [MaxLength(128)]
        [MinLength(8)]
        [Required(ErrorMessage = "Email Address is required and used as your Login ID.")]
        [UIHint("email")] // type="email"
        public string   Email       { get; set; }

        public string   Name        { get; set; }

        [MaxLength(128)]
        [MinLength(6)]
        [Required(ErrorMessage = "A password is required of at least 8 characters.")]
        [UIHint("password")]
        public string   Password    { get; set; } = "";

        public bool     Member      { get; set; } = false;  // not nullable

        //  c o n s t r u c t o r s 
        //  m e t h o d s

    }
}
