namespace Topmass.SyncData
{
    public class Program
    {
        static  void Main(string[] args)
        {
            ImportCV import = new ImportCV();
           var dataReport =  import.ImportCase();
            var handle = new HandleSyncImportCV(dataReport);
             handle.SyncData();
        }
    }
}
