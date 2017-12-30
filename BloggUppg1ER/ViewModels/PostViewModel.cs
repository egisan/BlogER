using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloggUppg1ER.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

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
        // [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? PostedOn { get; set; }

        // Properties below belongs to Category class
      
        // I need the ID below so I can return a value of the selected item 
        // from the view Create!
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "välj kategori namn")]
        [Display(Name = "Kategori namn")]

        // public SelectList Categories { get; set; }
        public List<SelectListItem> Categories { get; set; }

    }
}
