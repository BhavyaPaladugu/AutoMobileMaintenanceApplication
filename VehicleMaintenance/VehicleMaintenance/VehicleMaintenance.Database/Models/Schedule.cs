using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleMaintenance.DAL.CustomValidators;

namespace VehicleMaintenance.DAL.Models
{
    [Table("Schedule")]
    [CheckForProperAssociation("VehicleId", "TaskId")]
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [Required(ErrorMessage = "Vehicle Selection is required.")]
        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        [Required(ErrorMessage = "Task Selection is required.")]
        public int TaskId { get; set; }

        public Task Task { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedOrUpdatedDate { get; set; } = System.DateTime.Now;
    }
}