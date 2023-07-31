using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class TaskViewModel
    {
        public int ?TaskId { get; set; }

        [Required(ErrorMessage = "The Task Name field is required.")]
        public string Name { get; set; }
        public int Priority { get; set; }

        [Required(ErrorMessage = "The Status field is required.")]
        public string Status { get; set; }
        public List<SelectListItem> ?StatusList { get; set; }

    }
}
