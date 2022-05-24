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
        ScriptEnumTable(creationScript);
    }

    public void ScriptEnumTable(string tableScript)
    {
        var columns = Regex.Matches(tableScript, @"(?<=^\s*\`)[^`]*", RegexOptions.Multiline).OfType<Match>().Select(i => i.Value).ToList().ToCsv();
        if (columns == "Id,Key,Order")
        {
            var enumScripts = @$"
insert ignore into `{TableName}` (Id, `Key`, `Order`)
values ";
            var data = Database.Open(MasterConnection).Get(@$"select * from `{DatabaseName}`.`{TableName}`");
            foreach (DataRow row in data.Rows)
            {
                var query = @$"
({row["Id"].ToString()}, '{row["Key"].ToString()}',  {(row.IsNull("Order") ? "null" : row["Order"].ToString())}),";                
                enumScripts += query;
            }
            enumScripts = enumScripts.Trim().Trim(',');
            File.WriteAllText(Path.Combine(Dir, $"{TableFqn}.Data.sql"), enumScripts);
        }
    }
}