using ContactList.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Contact> Contacts { get; set; }
        ApplicationDbContext Create();
        int SaveChanges();
    }
   
}
