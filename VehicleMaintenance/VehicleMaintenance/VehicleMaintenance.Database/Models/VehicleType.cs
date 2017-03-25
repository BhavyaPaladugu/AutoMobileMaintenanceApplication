using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMaintenance.DAL.Models
{
    [Table("VehicleType")]
    public class VehicleType
    {
        [Key]
        public int VehicleTypeId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string VehicleTypeName { get; set; }
    }
}