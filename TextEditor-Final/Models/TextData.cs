using System.ComponentModel.DataAnnotations;
using TextEditor_Final.Areas.Identity.Data;

namespace TextEditor_Final.Models
{
    public class TextData
    {
        [Key]
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string Data { get; set; }
        public TextEditorUser User { get; set; }
    }
}
