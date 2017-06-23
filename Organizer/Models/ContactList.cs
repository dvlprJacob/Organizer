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
        public string Name { get; set; }
        public string Surname { get; set; }
        // Отчество, м.б. NULL
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        // Ограничение в формате : в БД записываются лишь картежи с значением поля 'word%',
        // где word из {Телефон,Email,Skype,Другое}, defoult value - 'Нет'
        public string Contacts { get; set; }
    }
}