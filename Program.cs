global using Holism.Infra;
global using System.Text.RegularExpressions;
global using Holism.DataAccess;
global using System.Data;

var connection = InfraConfig.GetConnectionString("Accounts");
var masterConnection = Regex.Replace(connection, @"database=.*", "");
// Logger.LogInfo(masterConnection);
var databases = Database.Open(masterConnection).Get("show databases");
foreach (DataRow database in databases.Rows)
{
    var databaseName = database["Database"].ToString();
    if (Char.IsLower(databaseName[0])) 
    {
        continue;
    }
    Logger.LogInfo(databaseName);
}