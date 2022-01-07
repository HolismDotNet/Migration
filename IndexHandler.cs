public class IndexHandler : Handler
{
    public void Handle()
    {
        var query = @$"
            show indexes from Contacts.Profiles
            where key_name not in 
            (
                select constraint_name
                from information_schema.referential_constraints
                where constraint_schema = 'Contacts'
                and table_name = 'Profiles'
            )
            and key_name != 'PRIMARY'
        ";
        var indexes = Database.Open(MasterConnection).Get(query);
        foreach (DataRow index in indexes.Rows)
        {
        }
    }
}