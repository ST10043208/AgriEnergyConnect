# AgriEnergyConnect

AgriEnergyConnect is an ASP.NET Core MVC application designed to connect farmers and buyers in the agricultural energy market. This application allows users to register, log in, and access functionalities based on their roles.

## Table of Contents
- [Setup Instructions](#setup-instructions)
- [Building and Running the Prototype](#building-and-running-the-prototype)
- [System Functionalities](#system-functionalities)
- [User Roles](#user-roles)

## Setup Instructions

### Prerequisites
1. **.NET SDK**: Ensure you have the .NET SDK (version 8.0 or higher) installed. You can download it from [here](https://dotnet.microsoft.com/download).
2. **IDE**: Use an Integrated Development Environment (IDE) like Visual Studio 2022 or Visual Studio Code.
3. **SQL Server**: Install SQL Server or SQL Server Express for the database.

### Step-by-Step Instructions

1. **Clone the Repository**:
   ```sh
   git clone https://github.com/ST10043208/AgriEnergyConnect
   cd AgriEnergyConnect
   ```

2. **Set Up the Database**:
-Open the appsettings.json file and configure the connection string to your SQL Server instance:			
 json
-Exchange your_server_name with your SQL Server instance name. The program will automatically make a new database with the correct name.
```sh
"ConnectionStrings": {
  "DefaultConnection": "Server=your_server_name;Database=AgriEnergyConnectDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
-Alternatively, you can use a local database by changing the connection string to (localdb)\\mssqllocaldb:
 json
```sh
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AgriEnergyConnectDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
-Unfortunately data will not persist if you use a local database.
-I used a local database but functionality will still be there you might just need to seed your own data.
-Open the Program.cs file and ensure it includes the database creation logic

3. **Build the application**																	
Open packaage manager console and run the following command:
```sh
dotnet build
```

4. **Run the application**:
Open packaage manager console and run the following command:
```sh
dotnet run
```

### Building and Running the Prototype
**Building**:
-Open the solution in Visual Studio and build the project using Ctrl+Shift+B.
-Alternatively, use the command line to build the project with dotnet build.

**Running**
-In Visual Studio, press F5 to run the project.
-Using the command line, navigate to the project directory and execute dotnet run.

### System Functionalities
-User Registration and Login
-Users can register and log in to access different parts of the application.
-User roles determine the accessible features.

1. **Farmers Management**
-View Farmers: Display a list of all registered farmers.
-Create Farmer: Add new farmers to the system.
-Edit Farmer: Update farmer details.
-Delete Farmer: Remove farmers from the system.
-Search Functionality

2. **Products Management**
-View Products: Display a list of all available products.
-Create Product: Add new products to the system.
-Edit Product: Update product details.
-Delete Product: Remove products from the system.
-Search Functionality

3. **Dark Mode**
-Toggle between light and dark mode for better accessibility.

4. **Logout Confirmation**
-Users will be asked to confirm before logging out.

### User Roles
**Employees**
-Full access to all functionalities.
-Manage farmers and products.
**Farmer**
-Manage products.

