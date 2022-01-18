using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavenegar.TechnicalEvaluation.FileManager.Manager
{
    public interface IExcelManager
    {
        public Task<string> BulkInsertChunkedExcelToDbAsync();
    }
}
