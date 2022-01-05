public class DatabaseHandler : Handler
{
    public void Handle()
    {
        File.WriteAllText(Path.Combine(DatabaseDir, "Database.sql"), @$"
            create database if not exists {DatabaseName}
        ");
        var tables = Database.Open(MasterConnection).Get(@$"show tables from {DatabaseName}");
        foreach(DataRow table in tables.Rows)
        {
            TableName = table["Tables_in_Contacts"];
            new TableHandler().Handle();
        }
    }
}