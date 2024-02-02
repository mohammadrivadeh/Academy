using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AcademyEFCore_NET7.Models
{
    public class Presentation
    {
        [Key]
        public int PID { get; set; }
        public string Name { get; set; }
        [ForeignKey("Course")]
        public int CID { get; set; }
        [ForeignKey("Teacher")]
        public int TID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public Course Course { get; set; }
        public Teacher Teacher { get; set; }
        public virtual ICollection<Register> Registers { get; set; }
    }
}
