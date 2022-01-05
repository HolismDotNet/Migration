public class Handler
{
    public static string Organization { get; set; }

    public static string OrganizationPrefix { get; set; }

    public static string Repository { get; set; }

    public static string RepositoryPath { get; set; }

    public static string MasterConnection { get; set; }

    public static string DatabaseName { get; set; }

    public static string TableName { get; set; }

    public static string DatabaseDir
    {
        get
        {
            var path = $"/{Organization}/Migration/Dev/{DatabaseName}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }

    public static string TableDir
    {
        get
        {
            var path = Path.Combine(DatabaseDir, TableName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}