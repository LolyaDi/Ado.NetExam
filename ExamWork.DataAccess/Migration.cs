using DbUp;
using System.Configuration;
using System.Reflection;

namespace ExamWork.DataAccess
{
    public class Migration
    {
        private readonly string _connectionString;

        public Migration()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
        }

        public void CheckMigration()
        {
            EnsureDatabase.For.SqlDatabase(_connectionString);

            var upgrader = DeployChanges.To
                .SqlDatabase(_connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful) throw new System.Exception("Ошибка соединения!");
        }
    }
}
