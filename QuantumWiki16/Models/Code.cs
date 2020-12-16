using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantumWiki16.Models
{
    [Table("Codes")]
    public class Code
    {
        // F i e l d s   &   P r o p e r t i e s
        
        [Key]
        public int     CodeId          { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string  Title           { get; set; }
        public string  Description     { get; set; }
        [ForeignKey("Users")]
        public int     Id              { get; set; }
        [ForeignKey("Languages")]
        public int     LangId          { get; set; }
        [Required(ErrorMessage = "Code is required.")]
        public string  CodeSegment     { get; set; }

    }
}
