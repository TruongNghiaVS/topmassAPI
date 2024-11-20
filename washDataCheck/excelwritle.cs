using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace washDataCheck
{
    public class Excelwritle
    {
        DataGlobal _dataGlobal;

        public Excelwritle()
        {
            _dataGlobal = DataGlobal.Instance;
        }

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
        public void LoadData()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var file = new FileInfo("C:\\Users\\Admin\\Desktop\\miraethang11\\washdata.xlsx");
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];

                int totalRows = workSheet.Rows.Count();
                for (int i = 2; i <= totalRows; i++)
                {
                    var noAgrre = ReadvalueStringExcel(workSheet, i, 1);
                    var customerName = ReadvalueStringExcel(workSheet, i, 2);
                    var phoneNumber = ReadvalueStringExcel(workSheet, i, 3);
                    if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length < 5)
                    {
                        continue;
                    }
                    var itemdata = new InputCase()
                    {
                        NoAgree = noAgrre,
                        Phone = phoneNumber,
                        CustomerName = customerName
                    };
                    _dataGlobal.AddInput(itemdata);
                }
            }

        }
        public void WritedFile()
        {
            var newFile = new FileInfo("C:\\Users\\Admin\\Desktop\\miraethang11\\outputResult.xlsx");

            File.Delete("C:\\Users\\Admin\\Desktop\\\\miraethang11\\outputResult.xlsx");
            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                var workSheet = xlPackage.Workbook.Worksheets.Add("Sheet1");
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 12;
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells[1, 1].Value = "SHĐ";
                workSheet.Cells[1, 2].Value = "Tên khách hàng";
                workSheet.Cells[1, 3].Value = "SĐT";
                workSheet.Cells[1, 4].Value = "Gọi lần 1";
                workSheet.Cells[1, 5].Value = "Gọi lần 2";
                workSheet.Cells[1, 6].Value = "Gọi lần 3";
                workSheet.Cells[1, 7].Value = "Duration";
                workSheet.Cells[1, 8].Value = "kết luận";
                int inderow = 2;
                workSheet.Row(inderow).Style.Font.Bold = false;

                foreach (var item in _dataGlobal.DataInput)
                {
                    var phoneNumber = item.Phone;
                    var codeStatus = "";
                    var duration = 0;
                    var dataResult = _dataGlobal.DataResult.Where(x => x.Phone == phoneNumber).ToList();
                    var kl = "";

                    workSheet.Cells[inderow, 1].Value = item.NoAgree;
                    workSheet.Cells[inderow, 2].Value = item.CustomerName;
                    workSheet.Cells[inderow, 3].Value = item.Phone;
                    var collumnIndex = 4;

                    var status1 = "";
                    var durationStatus = 0;

                    var isTalk = false;
                    var isConnect = false;
                    var isDie = false;

                    foreach (var item1 in dataResult)
                    {
                        if (item1.Disposition == "ANSWERED")
                        {
                            isTalk = true;
                        }

                        if (item1.Disposition == "BUSY")
                        {
                            isConnect = true;
                        }

                        if (item1.Disposition == "NO ANSWER")
                        {
                            isDie = true;
                        }
                        if (item1.Duration > 0)
                        {
                            durationStatus = item1.Duration;
                        }
                        codeStatus = item1.Disposition;
                        workSheet.Cells[inderow, collumnIndex].Value = codeStatus;

                        collumnIndex++;

                    }

                    var klcc = "chết";

                    if (isTalk == true)
                    {
                        klcc = "Khách hàng có nhấc máy và đổ chuông";
                    }
                    else if (isConnect == true)
                    {
                        klcc = "Khách hàng bận, hoặc thuê bao, tổng đài";
                    }
                    else if (isDie == true)
                    {
                        if (duration > 20)
                        {
                            klcc = "Số chết, nhưng vẫn có thể liên lạc được";
                        }
                        else
                        {
                            klcc = "Số chết, khó khăn liên hệ khách hàng";
                        }

                    }



                    workSheet.Cells[inderow, 7].Value = durationStatus;
                    workSheet.Cells[inderow, 8].Value = klcc;
                    inderow++;
                }
                xlPackage.Save();
            }
        }
    }
}
