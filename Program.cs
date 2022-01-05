global using Holism.Infra;
global using System.Text.RegularExpressions;

var connection = InfraConfig.GetConnectionString("Accounts");
var masterConnection = Regex.Replace(connection, @"database=.*", "");
Logger.LogInfo(masterConnection);