using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Organizer.Models
{
    public class ContactList
    {
        [Key]
        public long Id { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        // Отчество, м.б. NULL
        [DataType(DataType.Text)]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Организация")]
        public string Organization { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Должность")]
        public string Position { get; set; }
        // Ограничение в формате : в БД записываются лишь картежи с значением поля 'word%',
        // где word из {Телефон,Email,Skype,Другое}, defoult value - 'Нет'
        [DataType(DataType.Text)]
        [Display(Name = "Контакты")]
        public string Contacts { get; set; }
    }
}