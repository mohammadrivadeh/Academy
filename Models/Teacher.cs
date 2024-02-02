using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyEFCore_NET7.Models
{
    public class Teacher
    {
        [Key]
        public int TID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Presentation> Presentations { get; set; }

    }
}
