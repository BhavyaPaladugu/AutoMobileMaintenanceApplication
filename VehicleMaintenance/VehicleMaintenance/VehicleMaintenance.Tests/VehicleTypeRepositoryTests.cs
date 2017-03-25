using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using VehicleMaintenance.DAL.DataAccess;
using VehicleMaintenance.DAL.DataAccess.Contract;
using VehicleMaintenance.DAL.Models;

namespace VehicleMaintenance.Tests
{
    [TestClass]
    public class VehicleTypeRepositoryTests
    {
        [TestMethod]
        public void VehicleTypeTestsAllInOneMethod()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var fixture = new Fixture();
                var autoVehicleType = fixture.Create<VehicleType>();
                IVehicleTypeRepository vtRep = new VehicleTypeRepository();
                vtRep.Save(autoVehicleType);

                var insertedVehicleType = vtRep.GetById(autoVehicleType.VehicleTypeId);
                Assert.AreEqual(insertedVehicleType.VehicleTypeName, autoVehicleType.VehicleTypeName);

                IEnumerable<VehicleType> vtList = vtRep.GetAll().Where(v => v.VehicleTypeId == autoVehicleType.VehicleTypeId);
                Assert.AreEqual(vtList.Count(), 1);

                vtRep.Delete(autoVehicleType);
                var deletedVehicleType = vtRep.GetById(autoVehicleType.VehicleTypeId);
                Assert.IsNull(deletedVehicleType);
            }
        }
    }
}
