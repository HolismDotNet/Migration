public class ViewHandler : Handler
{
    public void Handle()
    {
        var query = @$"
            show create view `{DatabaseName}`.`{TableName}`
        ";
        var script = Database.Open(MasterConnection).Get(query).Rows[0][1].ToString();
        File.WriteAllText(Path.Combine(Dir, $"{TableFqn}.sql"), script);
    }
}