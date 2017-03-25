using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using VehicleMaintenance.DAL.Models;
using VehicleMaintenance.Database.DataAccess;
using VehicleMaintenance.Database.DataAccess.Contract;

namespace VehicleMaintenance.Tests
{
    [TestClass]
    public class VehicleRepositoryTests
    {
        [TestMethod]
        public void VehicleTestsAllInOneMethod()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var fixture = new Fixture();
                var autoVehicle = fixture.Create<Vehicle>();
                IVehicleRepository vRep = new VehicleRepository();
                vRep.Save(autoVehicle);

                var insertedVehicle = vRep.GetById(autoVehicle.VehicleId);
                Assert.AreEqual(insertedVehicle.VehicleType, autoVehicle.VehicleType);
                Assert.AreEqual(insertedVehicle.Make, autoVehicle.Make);

                IEnumerable<Vehicle> vList = vRep.GetAll().Where(v => v.VehicleId == autoVehicle.VehicleId);
                Assert.AreEqual(vList.Count(), 1);

                vRep.Delete(autoVehicle);
                var deletedVehicle = vRep.GetById(autoVehicle.VehicleId);
                Assert.IsNull(deletedVehicle);
            }
        }
    }
}
