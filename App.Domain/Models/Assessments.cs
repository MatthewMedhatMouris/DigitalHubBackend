using App.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public class Assessments : BaseEntity
    {
        public string title { get; set; }
        public string short_description { get; set; }
        public string description { get; set; }
        public string slug { get; set; }
        public int created_by { get; set; }
        public int updated_by { get; set; }
        public int category_id { get; set; }
    }
}
