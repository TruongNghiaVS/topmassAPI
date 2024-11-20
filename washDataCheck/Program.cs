namespace washDataCheck
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var dataGlobale = DataGlobal.Instance;
            Console.WriteLine("load data input");
            var fileexcel = new Excelwritle();
            fileexcel.LoadData();
            Console.WriteLine("kết thúc load data input");
            Console.WriteLine(dataGlobale.DataInput.Count);
            Console.WriteLine("load data result");
            var dataacess = new DataAccess();
            dataacess.GetDataWashData();
            Console.WriteLine("kết thúc load data ");
            Console.WriteLine("wirthe file excel ");
            fileexcel.WritedFile();
        }
    }
}
