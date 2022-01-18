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
        // new ColumnHandler().Handle();
        // new ForeignKeyHandler().Handle();
    }

    public void ScriptEnumTable(string tableScript)
    {
        var columns = Regex.Matches(tableScript, @"(?<=^\s*\`)[^`]*", RegexOptions.Multiline).OfType<Match>().Select(i => i.Value).ToList().ToCsv();
        if (columns == "Id,Guid,Key,Order")
        {
            var enumsScripts = new List<string>();
            var data = Database.Open(MasterConnection).Get(@$"select * from `{DatabaseName}`.`{TableName}`");
            foreach (DataRow row in data.Rows)
            {
                
            }
        }
    }
}