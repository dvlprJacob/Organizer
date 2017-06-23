using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Organizer.Models
{
    public class Diaryes
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.Text)]
        public virtual long Id { get; set; }

        // Тип записи : {Встреча, Дело, Памятка}
        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.Text)]
        [Display(Name = "Тип")]
        public virtual string Type { get; set; }
        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Тема")]
        public virtual string Theme { get; set; }
        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Начало")]
        public virtual DateTime BeginDate { get; set; }

        // NULL для записи с Type = 'Памятка'
        [DataType(DataType.DateTime)]
        [Display(Name = "Конец")]
        public virtual DateTime? EndDate { get; set; }
        [Display(Name = "Статус ( 0 или 1)")]
        public virtual long DoneStatus { get; set; }
    }
}
