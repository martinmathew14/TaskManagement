using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Model
{
    public class Task
    {
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Task Name Is Required.")]
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
    }
}
