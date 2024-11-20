using OfficeOpenXml;
using Topmass.Utility;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Topmass.SyncData
{
    public class ImportCV
    {
        protected string ReadvalueStringExcel(ExcelWorksheet excelworksheet, int row, int col)
        {
            var cellRange = excelworksheet.Cells[row, col];
            if (cellRange != null)
            {

                if (cellRange.Value != null)
                {
                    return cellRange.Value.ToString();
                }
            }
            return "";
        }

        protected DateTime? ReadValueDateTimeExcel(ExcelWorksheet excelworksheet, int row, int col)
        {
            DateTime? resultInput = null;
            var cellRange = excelworksheet.Cells[row, col];
            if (cellRange != null)
            {

                if (cellRange.Value != null)
                {
                    try
                    {

                        resultInput = DateTime.Parse(cellRange.Value.ToString());
                    }
                    catch (Exception e)
                    {
                        return resultInput;

                    }

                }
            }
            return resultInput;
        }
        public List<CVInfoImport> ImportCase()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var listResult = new List<CVInfoImport>();

            var file = new FileInfo("C:\\Users\\Admin\\Desktop\\syncdata\\finalData.xlsx");

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                int totalRows = workSheet.Rows.Count();
                for (int i = 2; i <= totalRows; i++)
                {
                    ReadvalueStringExcel(workSheet, i, 1);
                    var itemdata = new CVInfoImport()
                    {
                        Name = ReadvalueStringExcel(workSheet, i, 1),
                        Phone = ReadvalueStringExcel(workSheet, i, 2),
                        Email = ReadvalueStringExcel(workSheet, i, 3),
                        Location = ReadvalueStringExcel(workSheet, i, 4),
                        Gender = ReadvalueStringExcel(workSheet, i, 5),
                        Dob = ReadValueDateTimeExcel(workSheet, i, 6),
                        Postion = ReadvalueStringExcel(workSheet, i, 7),
                        LinkFileCV = ReadvalueStringExcel(workSheet, i, 8),
                        FileCV2 = ReadvalueStringExcel(workSheet, i, 9),
                        Introduction = ReadvalueStringExcel(workSheet, i, 10),
                        Content = ReadvalueStringExcel(workSheet, i, 11),
                        EducationLevel = ReadvalueStringExcel(workSheet, i, 12)
                    };
                    itemdata.Content2 = Utilities.RemoveSign4VietnameseString(itemdata.Content);
                    listResult.Add(itemdata);
                }
            }
            return listResult;

        }
    }
}
