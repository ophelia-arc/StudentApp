namespace StudentApp
{
    static class Db
    {
        public const string ConnStr =
            @"Server=.\SQLEXPRESS; Database=UniversityDB; Trusted_Connection=True; TrustServerCertificate=True;";

        public static string LibraryConnStr =
            @"Server=(local)\SQLEXPRESS;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;";
    }
}