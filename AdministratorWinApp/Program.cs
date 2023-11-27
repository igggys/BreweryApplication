namespace AdministratorWinApp
{
    internal static class Program
    {
        private static Mutex m_instance;
        private const string m_appName = "AdministratorWinApp";

        [STAThread]
        static void Main()
        {
            bool tryCreateNewApp;
            m_instance = new Mutex(true, m_appName, out tryCreateNewApp);
            if (tryCreateNewApp)
            {
                ApplicationConfiguration.Initialize();
                Application.Run(new EnterForm());
            }
        }
    }
}