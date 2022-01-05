public class TableHandler : Handler
{
    public void Handle()
    {
        var query = @$"
            create table if not exists {TableFqn}
        ";
        File.WriteAllText(Path.Combine(Dir, $"{TableFqn}.sql"), query);
        new ColumnHandler().Handle();
        new ForeignKeyHandler().Handle();
    }
}