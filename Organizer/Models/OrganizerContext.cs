using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Organizer.Models
{
    public class OrganizerContext:DbContext
    {
        public OrganizerContext() : base("Organizer")
        {
        }
        public DbSet<Diaryes> Diary { get; set; }
        public DbSet<ContactList> Contact { get; set; }
    }
}