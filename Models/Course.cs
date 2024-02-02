using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyEFCore_NET7.Models
{
    public class Course
    {
        [Key]
        public int CID { get; set; }
        public string CName { get; set; }
        public string Category { get; set; }

        public virtual ICollection<Presentation> Presentations { get; set; }
    }   
}
