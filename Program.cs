namespace VoxelStoragePerformanceComparison
{
    internal class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize(); // For .NET 6 and above
            Application.Run(new Form1());
        }
    }
}