using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavenegar.TechnicalEvaluation.FileManager.Data
{
    internal class FileRepository : IFileRepository
    {
        private readonly string _connectionString;
        public FileRepository()
        {
            _connectionString = ConfigurationManager.DefaultConnectionString;
        }
        public async Task SaveExcellToDb(DataTable smsDataTable)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock, null))
                    {
                        bulkCopy.DestinationTableName = "SMSContent";//"SMSContent";
                        bulkCopy.ColumnMappings.Add(0, "Body");
                        bulkCopy.BulkCopyTimeout = 60;
                        conn.Open();
                        await bulkCopy.WriteToServerAsync(smsDataTable);
                        bulkCopy.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
