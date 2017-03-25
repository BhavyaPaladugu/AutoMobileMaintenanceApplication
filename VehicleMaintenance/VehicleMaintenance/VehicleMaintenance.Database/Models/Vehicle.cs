using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMaintenance.DAL.Models
{
    [Table("Vehicle")]
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Please enter Make information of the Car.")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Please enter Model information of the Car.")]
        public string Model { get; set; }

        public int Year { get; set; }

        public int OdometerReading { get; set; }

        [Required(ErrorMessage = "Vehicle Type Selection is required.")]
        public int VehicleTypeId { get; set; }

        public VehicleType VehicleType { get; set; }
     }
}