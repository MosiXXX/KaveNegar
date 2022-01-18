using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavenegar.TechnicalEvaluation.FileManager.Data
{
    public interface IFileRepository
    {
        public Task SaveExcellToDb(DataTable smsDataTable);
    }
}
