using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMaintenance.DAL.Models
{
    [Table("Task")]
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string TaskName { get; set; }
    }
}