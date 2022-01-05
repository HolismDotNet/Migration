public class Handler
{
    public static string Environment { get; set; }

    public static string Organization { get; set; }

    public static string OrganizationPrefix { get; set; }

    public static string Repository { get; set; }

    public static string RepositoryPath { get; set; }

    public static string MasterConnection { get; set; }

    public static string DatabaseName { get; set; }

    public static string TableName { get; set; }

    public static string TableFqn
    {
        get
        {
            return $"{DatabaseName}.{TableName}";
        }
    }

    public static string Dir
    {
        get
        {
            var path = $"/{Organization}/Migration/{Environment}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}