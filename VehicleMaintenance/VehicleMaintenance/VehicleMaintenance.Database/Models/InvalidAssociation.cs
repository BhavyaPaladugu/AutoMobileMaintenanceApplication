using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMaintenance.DAL.Models
{
    [Table("InvalidAssociation")]
    public class InvalidAssociation
    {
        [Key]
        public int InvalidAssociationId { get; set; }

        [Required(ErrorMessage = "Vehicle Type Selection is required.")]
        public int VehicleTypeId { get; set; }

        public VehicleType VehicleType { get; set; }

        [Required(ErrorMessage = "Task Selection is required.")]
        public int TaskId { get; set; }

        public Task Task { get; set; }
    }
}