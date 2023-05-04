# Machine Health Check

A .Net 6 web project that utilzes SignalR to remotely check OS, CPU, RAM, Disk, and SQL info on another Windows machine. Also provided is a sleek Angular UI to index and perform health checks on connected machines. 

# How to run locally
Requirements:
* Angular
* Visual Studio 22
* Microsoft SQL Server

1. Open the project in Visual Studio, the .sln is inside MachineHealthCheck/App.
3. Install the required NuGet and NPM packages.
4. Create a database for the project in Microsoft SQL Server and set the connection string in MachineHealthCheck/App/appsetting.json. You do not need to create any tables manually.
5. Set MachineHealthCheck.API, HealthCheck.Host, and HealthCheck.Remote as startup projects.
6. Click "Start" and wait for the Swagger windows and HealthCheck consoles to start. 
7. In the Visual Studio developer Powershell console, navigate to the MachineHealthCheck.Web directory. then run "ng serve --open", this runs the frontend.

You can now use the Angular UI to add a machine to your machine list. make up a key for the machine and enter this key in the HealthCheck.Remote console (it says "Please enter the key provided to you"). Once you enter your key and you see "Connected to host", you can use the UI to run a health check on the machine and view the results. 

