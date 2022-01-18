using Microsoft.Extensions.Configuration;

namespace Kavenegar.TechnicalEvaluation.FileManager
{
    public static class ConfigurationManager
    {
        public static string DefaultConnectionString => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build()
            .GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        public static int MaxRecordToBulkInsert => 50000;
    }
}