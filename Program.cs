﻿global using Holism.Infra;
global using System.Text.RegularExpressions;
global using Holism.DataAccess;
global using System.Data;
global using System.IO;
global using System.Linq;

var connection = InfraConfig.GetConnectionString("Accounts");
var masterConnection = Regex.Replace(connection, @"database=.*", "");
// Logger.LogInfo(masterConnection);
var databases = Database.Open(masterConnection).Get("show databases");
Handler.Environment = InfraConfig.GetEnvironmentVariable("Environment");
Handler.MasterConnection = masterConnection;
Handler.RepositoryPath = args[0];
Handler.Organization = args[1];
Handler.OrganizationPrefix = args[2];
Handler.Repository = args[3];
foreach (DataRow database in databases.Rows)
{
    var databaseName = database["Database"].ToString();
    if (Char.IsLower(databaseName[0])) 
    {
        continue;
    }
    Handler.DatabaseName = databaseName;
    // Logger.LogInfo(databaseName);
    new DatabaseHandler().Handle();
}