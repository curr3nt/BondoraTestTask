# Bondora test task implementation
This repo contains code implementation of a Bondora test task.

Main projects are BackendTcpService and rontend. **BackendTcpService** is a simple *TCP server* which works with custom-designed message class. **Frontend** is a *ASP .NET MVC 4 project*.
## Launching
**BackendTcpService** runs by default at *127.0.0.1:55007* a as console project: `F5 -> BackendTcpService.Program.cs`. 
Persistance is organized through EF using LocalDB as a storage, so please make sure you have installed *LocalDB* provider for your *SQL Server*. Also please make sure to change the database name in the `App.config` file if you already have a LocalDb named *Database1*.

**Frontend** can be runned off *Local IIS Web server* and runs by default at port *55006*.

## Projects description
* *BackendTcpService*

   Describes a TCP server that accepts messages, processes them and returns a response. Logs its activity (using a [NLog](http://nlog-project.org/)).

* *BackendTcpService.Protocol*

   Describes a message class which is used by server and client as a datagramm.

* *Business logic*

   This project works with data layer and handles all busines logic of the application.

* *DAL*

   This project works with EF. Technically, this project is tightly coupled with **Business logic** but this is incorrect, as data layer (as any other layer) should be coupled loosely, e.g. this makes it very difficult to write tests.

* *Domain*

   Business entities of this system.

* *Domain.Tests*

   Unit test for **Domain** classes

* *DTO*

   Data transfer objects. Wrap around **Domain** objects for transferring between frontend and backend. Actually, they are used as a model for views as well, but this was done for simplicity's sake, and *ViewModels* should be created for this purpose.

* *Frontend*
 
   ASP .NET MVC application, user interface.