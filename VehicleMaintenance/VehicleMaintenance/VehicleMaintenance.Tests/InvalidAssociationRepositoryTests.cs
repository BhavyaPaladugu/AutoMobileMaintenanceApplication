using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using VehicleMaintenance.DAL.Models;
using VehicleMaintenance.DAL.DataAccess.Contract;
using VehicleMaintenance.DAL.DataAccess;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;

namespace VehicleMaintenance.Tests
{
    [TestClass]
    public class InvalidAssociationRepositoryTests
    {
        [TestMethod]
        public void InvalidAssociationsAllTestsInOneMethod()
        {
            using (TransactionScope scope = new TransactionScope())
            {                
                var fixture = new Fixture();
                var autoInvAssociation = fixture.Create<InvalidAssociation>();
                IInvalidAssociationRepository invRep = new InvalidAssociationRepository();
                invRep.Save(autoInvAssociation);

                var insertedInvAssociation = invRep.GetById(autoInvAssociation.InvalidAssociationId);
                Assert.AreEqual(insertedInvAssociation.VehicleType, autoInvAssociation.VehicleType);
                Assert.AreEqual(insertedInvAssociation.Task.TaskName, autoInvAssociation.Task.TaskName);

                IEnumerable<InvalidAssociation> invAssList = invRep.GetAll().Where(iv => iv.InvalidAssociationId == autoInvAssociation.InvalidAssociationId);
                Assert.AreEqual(invAssList.Count(), 1);

                invRep.Delete(autoInvAssociation);
                var deletedInvAssociation = invRep.GetById(autoInvAssociation.InvalidAssociationId);
                Assert.IsNull(deletedInvAssociation);
            }
        }
    }
}
