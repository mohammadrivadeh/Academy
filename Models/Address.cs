using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyEFCore_NET7.Models
{
    public class Address
    {
        [Key]
        public int AID { get; set; }
        public int SID { get; set; }
        public string FullAddress { get; set; }
        public string PostalCode { get; set; }
        public Student Student { get; set; }

    }
}
