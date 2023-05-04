# Machine Health Check

A .Net 6 web project that utilzes SignalR to remotely check OS, CPU, RAM, Disk, and SQL info on another Windows machine. Also provided is a sleek Angular UI to index and perform health checks on connected machines. 

# How to run locally
Requirements:
* Angular
* Visual Studio 22
* Microsoft SQL Server

1. Open the project in Visual Studio, the .sln is inside MachineHealthCheck/App

2. Install the required NuGet and NPM packages

3. Create a database for the project in Microsoft SQL Server and set the connection string in MachineHealthCheck/App/MachineHealthCkeck.API/appsetting.json. You do not need to create any tables manually

4. Set MachineHealthCheck.API, HealthCheck.Host, and HealthCheck.Remote as startup projects

5. Click "Start" and wait for the Swagger windows and HealthCheck consoles to start

6. In the Visual Studio developer Powershell console, navigate to the MachineHealthCheck.Web directory. then run "ng serve --open", this runs the frontend

You can now use the Angular UI to add a machine to your machine list. make up a key for the machine and enter this key in the HealthCheck.Remote console (it says "Please enter the key provided to you"). If you see "Connected to Host" after entering the key you are ready to perform health checks. Navigate back to the UI and click the "Check Details" button on your machine. To perfrom the health check, click the "Check Machine" button inside of the details dialog. wait for the busy indicator to finish (the check may take ~10 seconds) and you can view the health check results.

