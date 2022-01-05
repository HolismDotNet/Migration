public class Handler
{
    public static string Organization { get; set; }

    public static string OrganizationPrefix { get; set; }

    public static string Repository { get; set; }

    public static string RepositoryPath { get; set; }

    public static string MasterConnection { get; set; }

    public static string Database { get; set; }

    public static string Dir
    {
        get
        {
            var path = $"/{Organization}/Migration/Dev/{Database}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}