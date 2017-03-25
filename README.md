# AutomobileMaintenanceSoftware
Its a ASP.NET MVC application .It supports  adding,viewing,updating and deleting  of all tasks related to automobile maintenance.
# Internal Design
I choose to implement given requirement in MVC with Entity framework code first approach.Because my database schema is fully based on my code models.Instead of creating script files and send it to you,it will automatically generate script files and will be stored to local db in VS.So,no need of creating database and scripts while you are testing my application.

Here,Entity framework (VMEntities in the code) acts as dataaccesslayer.

Created IRepositories for business logic methods (mainly CRUD operations) and implemented them for future extension of application.

I created unit test stubs to test the main CRUD operations in sequence.


*****************************************************THANK YOU***************************************************************************
