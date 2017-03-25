namespace VehicleMaintenance.DAL
{
    using Models;
    using System.Data.Entity;

    public class VMEntities : DbContext
    {
        // Your context has been configured to use a 'VMEntities' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'VehicleMaintenance.VMEntities' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'VMEntities' 
        // connection string in the application configuration file.
        public VMEntities()
            : base("name=VMEntities")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<InvalidAssociation> InvalidAssociations { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
