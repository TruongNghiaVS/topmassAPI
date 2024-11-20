namespace TopMass.Import
{
    using Dapper;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;

    public class ProvinceImport
    {
        public static void AddItem(Regional itemInsert, int typeInser = 1)
        {
            if (string.IsNullOrEmpty(itemInsert.Code) || string.IsNullOrEmpty(itemInsert.Name))
            {
                return;
            }
            var code = itemInsert.Code;
            var name = itemInsert.Name;
            var slug = Utiliti.ToUrlSlug(itemInsert.Name);
            itemInsert.Slug = slug;

            try
            {
                using (SqlConnection conn = new SqlConnection("Server=192.168.1.3,1433; Initial Catalog=jobvieclam;User ID=crm;Password=Vietstar@2018; Persist Security Info=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Integrated Security=false;"))
                {
                    conn.Execute("sp_regional_insert", itemInsert, commandType: System.Data.CommandType.StoredProcedure);
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Occre while creating table:" + e.Message + "\t" + e.GetType());
            }
        }

        public void ImportProvice()
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Admin\Desktop\TinhThanh\tinhthanh.xls");
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            var listData = new List<Regional>();
            for (int i = 2; i <= rowCount; i++)
            {
                string fullName = xlRange.Cells[i, 2].Value2.ToString();
                fullName = fullName.Replace("Tỉnh", string.Empty);
                var regional = new Regional()
                {
                    Code = xlRange.Cells[i, 1].Value2.ToString(),
                    Name = fullName,
                    Datatype = 1
                };
                listData.Add(regional);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            foreach (var item in listData)
            {
                AddItem(item);
            }
        }

        public void ImportDir()
        {

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Admin\Desktop\TinhThanh\danhsachcaphuyen.xls");
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            var listData = new List<Regional>();
            for (int i = 2; i <= rowCount; i++)
            {
                var regional = new Regional()
                {
                    Code = xlRange.Cells[i, 1].Value2.ToString(),
                    Name = xlRange.Cells[i, 2].Value2.ToString(),
                    Datatype = 2,
                    Level1 = xlRange.Cells[i, 5].Value2.ToString(),
                    //Level2 = xlRange.Cells[i, 7].Value2.tostring(),

                };

                listData.Add(regional);

            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            foreach (var item in listData)
            {

                AddItem(item);
            }
        }
        public void ImportReward()
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Admin\Desktop\TinhThanh\xaphuong.xls");
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            var listData = new List<Regional>();
            for (int i = 2; i <= rowCount; i++)
            {
                var regional = new Regional();
                regional.Code = xlRange.Cells[i, 1].Value2;
                regional.Name = xlRange.Cells[i, 2].Value2;
                regional.Datatype = 3;
                regional.Level2 = xlRange.Cells[i, 5].Value2;
                regional.Level1 = xlRange.Cells[i, 7].Value2;
                listData.Add(regional);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            foreach (var item in listData)
            {
                AddItem(item);
            }
        }
    }
}
