using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyEFCore_NET7.Models
{
    public class Register
    {
        [Key]
        public int RID { get; set; }
        [ForeignKey("Presentation")]
        public int PID { get; set; }
        [ForeignKey("Student")]
        public int SID { get; set; }
        public int Score { get; set; }
        public Student Student { get; set; }
        public Presentation Presentation { get; set; }
    }
}
