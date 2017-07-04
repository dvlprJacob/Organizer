using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Organizer.Models
{
    public class ContactListContext:DbContext
    {
        public DbSet<ContactList> Contact { get; set; }
    }
}