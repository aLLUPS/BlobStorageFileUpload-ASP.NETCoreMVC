# BlobUploadWebApp
This repository contains an ASP .NET Core MVC Web Application project intergrated with Azure Blob Storage.

*After cloning(downloading) the project, please make the following changes the relavent files:*

1. **apsettings.json**

    Insert the connection string to your Microsoft SQL database replacing 'ConectionStringToSQLDB' at line 3. This is the connection string with the name 'DefaultConnectioin'. (do not delete the double quotes)

2. **BlobUtility.cs**

    Insert the connection string of your Azure storage account replacing 'ConnectionStringToBlobStorage' at line 18. This is the connection string to connect the web app with Azure Blob Storage. (do not delete the double quotes)

3. **UserMediaController.cs**

    Insert the name of the storage account replacing the 'StorageAccountName' at line 30. (do not delete the double quotes)
  
    Insert the Access key of your storage account replacing the 'StorageAccountAccessKey' at line 31. (do not delete the double quotes)
  
    Insert the Name of the Blob container created in Azure blob storage, replacing the 'NameOfTheBlobContainer' at line 49 and 60 respectively. (do not delete the double quotes)
    
    <br>
    <br>
    
Then try the following command in NuGet Package Manger Console:

    update-database
    
*If it fails to build, Then follow the steps below:*

1. Delete the folder 'Migrations' with the all the contents in it.

3. Run the following command in NuGet Package Manger Console (This will create the migrations again)

    `add-migration BlobUploadWebApp-Migrations`
    
4. The run the following command again. (This will update the database creating the relavent classes)

   `update-database`
