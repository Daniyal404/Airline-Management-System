namespace Semester_4_Database_Systems_Project
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new AMS());
        }
    }
}