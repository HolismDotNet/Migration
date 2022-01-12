public class TableHandler : Handler
{
    public void Handle()
    {
        var query = @$"
            show create table `{DatabaseName}`.`{TableName}`
        ";
        var creationScript = Database.Open(MasterConnection).Get(query).Rows[0][1].ToString();
        creationScript = Regex.Replace(creationScript, @"ENGINE=InnoDB.*DEFAULT", "ENGINE=InnoDB DEFAULT");
        File.WriteAllText(Path.Combine(Dir, $"{TableFqn}.sql"), creationScript);
        // new ColumnHandler().Handle();
        // new ForeignKeyHandler().Handle();
    }
}