public class DatabaseHandler : Handler
{
    public void Handle()
    {
        File.WriteAllText(Path.Combine(Dir, $"{DatabaseName}.sql"), @$"
            create database if not exists {DatabaseName}
        ");
        var tables = Database.Open(MasterConnection).Get(@$"show tables from {DatabaseName}");
        foreach(DataRow table in tables.Rows)
        {
            TableName = table[$"Tables_in_{DatabaseName}"].ToString();
            new TableHandler().Handle();
        }
    }
}