using ExcelDataReader;
using Kavenegar.TechnicalEvaluation.FileManager.Constants;
using Kavenegar.TechnicalEvaluation.FileManager.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Kavenegar.TechnicalEvaluation.FileManager.Manager
{
    public class ExcelManager : IExcelManager
    {
        private readonly string _filePath;
        private readonly IFileRepository _fileRepository;
        private readonly int _maxRecordToBulkInsert;
        public ExcelManager(string filePath)
        {
            _filePath = filePath;
            _fileRepository = new FileRepository();
            _maxRecordToBulkInsert = ConfigurationManager.MaxRecordToBulkInsert;
        }

        /// <summary>
        /// this method will read excel file record after record and bulk insert into db async 
        /// </summary>
        /// <returns></returns>
        public async Task<string> BulkInsertChunkedExcelToDbAsync()
        {
            IExcelDataReader? reader = null;
            FileStream stream = File.Open(_filePath, FileMode.Open, FileAccess.Read);
            string result = string.Empty;
            try
            {

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                if (Path.GetExtension(_filePath).Equals(Constants.ExcelExtensionEnum.XLS.ToDescriptionString()))
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                else if (Path.GetExtension(_filePath).Equals(Constants.ExcelExtensionEnum.XLSX.ToDescriptionString()))
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            }
            catch (Exception)
            {
                result = "can't open file";
            }

            if (reader is null)
                return "empty";

            try
            {
                for (int i = 0; i < reader.RowCount; i += _maxRecordToBulkInsert)
                {
                    int recordCounter = 0;
                    DataTable tmpDataTable = new DataTable();
                    tmpDataTable.Columns.Add("body");
                    while (recordCounter++ < _maxRecordToBulkInsert && reader.Read())
                    {
                        tmpDataTable.Rows.Add(reader[0].ToString());
                    }

                    Task hh = _fileRepository.SaveExcellToDb(tmpDataTable);
                    await hh;
                }
            }
            catch (Exception)
            {
                result = "Failed to store in DB";
            }

            reader.Close();
            stream.Close();
            return "success";
        }


    }
}
