# How to run a variable in website

1. create a model > Declare type of variable (class/object)
2. Categories controller > declare variable + type > Based on variable in model > declare the values > pass it to View()
3. Index categories > request the varialbe

## Create a new method in Controller

1. Create a new method + View()
2. Add new > View > template > choose the template > add > build

## the difference between @model Project.Models.ModelNa

@model Project.Models.ModelName > kind of the key to all of the form generation code 

@model Thusday_lession1.Models.Category

Create based  on the category model 


## How to connect with azure DB

1. Create local DB (user same with windows)
2. Login portal.azure.com > create new db > choose package + save the admin account

3. Microsoft Server > View > Registered Servers > New Server registration
4. portal.azure.com >the DB server > fire wall > set new ip address > 0000 255255255255
5. input the database information 

## Create DB

1. List the tables /Models
2. List the properties / columns
3. Relationships between tables/ models

- Create diagram of relationships among models
- Setup Models
- Connect Relationship 

## Connecting the web project to the database

1. appsettings.json > 

- if using Windows Authentication for SQL server > "Server=your-computer-name;Database=your-db-name;IntegratedSecurity=True;"
- SQL Server Authentication - local
"Server=your-computer-name;Database=your-db-name;UserId=yourusername;Password=your-password"
- Azure
"Server=thusdaylession1.database.windows.net;Database=Thusday_lession1;User Id=thudba;Password=YGRenESR4vT9vD;"

2. Tools > NuGet package manager > Package Manager console > add-migration CreateFirstTables > update-database

**Everytime change in Models > run add-migration (same likes commit)** 