using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Data.Entity;

namespace VehicleMaintenance.DAL.CustomValidators
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class CheckForProperAssociationAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "An {0} car won't need an {1}";

        private readonly object _typeId = new object();

        public CheckForProperAssociationAttribute(string vehicleIdProperty, string taskIdProperty)
        : base(_defaultErrorMessage)
        {
            VehicleIdProperty = vehicleIdProperty;
            TaskIdProperty = taskIdProperty;
        }

        public string VehicleIdProperty
        {
            get;
            private set;
        }

        public string TaskIdProperty
        {
            get;
            private set;
        }

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }

        private int VehicleId { get; set; }
        private int TaskId { get; set; }

        public override string FormatErrorMessage(string name)
        {
            string vehicleTypeName = string.Empty;
            string taskName = string.Empty;
            
            using (VMEntities vm = new VMEntities())
            {
                vehicleTypeName = vm.Vehicles
                    .Include(x => x.VehicleType)
                    .SingleOrDefault(x => x.VehicleId == VehicleId)
                    .VehicleType.VehicleTypeName;
                taskName = vm.Tasks
                    .SingleOrDefault(x => x.TaskId == TaskId)
                    .TaskName;
            }
            return string.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                vehicleTypeName, taskName);
        }

        public override bool IsValid(object value)
        {
            bool retValue = true;
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            object vehicleIdValue = properties.Find(VehicleIdProperty.ToString(), true /* ignoreCase */).GetValue(value);
            object taskIdValue = properties.Find(TaskIdProperty.ToString(), true /* ignoreCase */).GetValue(value);

            VehicleId = Convert.ToInt32(vehicleIdValue);
            TaskId = Convert.ToInt32(taskIdValue);

            using (VMEntities vm = new VMEntities())
            {
                int VehicleTypeId = vm.Vehicles
                    .Include(x => x.VehicleType)
                    .SingleOrDefault(x => x.VehicleId == VehicleId)
                    .VehicleType.VehicleTypeId;
                foreach (Models.InvalidAssociation iv in vm.InvalidAssociations)
                {
                    if(iv.VehicleTypeId == VehicleTypeId && iv.TaskId == TaskId)
                    {
                        return false;
                    } 
                }
            }
            return retValue;
        }
    }
}