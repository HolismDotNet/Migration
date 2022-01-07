public class ForeignKeyHandler : Handler
{
    public void Handle()
    {
        var query = @$"
            select 
                rc.constraint_name,
                kcu.column_name,
                rc.referenced_table_name,
                kcu.referenced_column_name,
                update_rule,
                delete_rule
            from information_schema.referential_constraints rc
            inner join information_schema.key_column_usage kcu
            on rc.constraint_schema = kcu.table_schema
            and rc.table_name = kcu.table_name
            and rc.constraint_name = kcu.constraint_name
            where rc.constraint_schema = '{DatabaseName}'
            and rc.table_name = '{TableName}'
        ";
        var foreignKeys = Database.Open(MasterConnection).Get(query);
        foreach (DataRow foreignKey in foreignKeys.Rows)
        {
            var name = foreignKey["constraint_name"];
            var column = foreignKey["column_name"];
            var referencedTable = foreignKey["referenced_table_name"];
            var referencedColumn = foreignKey["referenced_column_name"];
            var update = foreignKey["update_rule"];
            var delete = foreignKey["delete_rule"];
            var definition = $"{name} {column} {referencedTable} {referencedColumn} {update} {delete}";
            File.WriteAllText(Path.Combine(Dir, $"{TableFqn}.{column}.FK.sql"), definition);
        }
    }
}