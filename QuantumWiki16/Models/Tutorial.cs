using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantumWiki16.Models
{
    [Table("Tutorials")]
    public class Tutorial
    {
        //  F i e l d s   a n d   p r o p e r t i e s 

        [Key]
        public int         TutId       { get; set; } // primary key. capitalize public 
        [Required(ErrorMessage = "Url is required.")]
        public string      Url         { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string      Title       { get; set; }
        public string      Subject     { get; set; }
        [ForeignKey("Users")]
        public int         UserId      { get; set; }

        //  c o n s t r u c t o r s 
        //  m e t h o d s
    }
}
