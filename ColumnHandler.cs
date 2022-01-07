public class ColumnHandler : Handler
{
    public void Handle()
    {
        var columns = Database.Open(MasterConnection).Get(@$"show columns from {TableFqn}");
        foreach(DataRow column in columns.Rows)
        {
            var columnName = column["Field"];
            var type = column["Type"];
            var isNullable = column["Null"].ToString() == "YES";
            var key = column["key"];
            var @default = column["default"];
            var extra = column["extra"];
            var definition = $"{columnName} {type} {(isNullable ? "null" : "not null")} {key} {@default} {extra}";
            File.WriteAllText(Path.Combine(Dir, $"{TableFqn}.{columnName}.sql"), definition);
        }
    }
}