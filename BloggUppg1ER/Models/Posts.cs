using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloggUppg1ER.Models
{
    public partial class Posts
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "Ange rubrik namn")]
        [StringLength(50)]
        [Display(Name = "Rubrik")]
        public string Title { get; set; }

        [StringLength(2000)]
        [Display(Name = "Inlägg")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Ange datum")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? PostedOn { get; set; }
        
        // This is a FK
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "välj kategori namn")]
        [Display(Name = "Kategori")]
        public virtual Categories Category { get; set; }
    }
}
