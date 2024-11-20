using System.Timers;

namespace washdata
{
    internal class Program
    {
        private static Queue<WashDataItem> DataWash;
        private static HandleWashdata handledata = new HandleWashdata();
        static void Main(string[] args)
        {
            var importFile = new ImportFile();
            importFile.ImportCase();
            importFile.LoadData();
            int i = 0;
            //while (importFile.DataList.Count > 0)
            //{
            //    i++;
            //    var item = importFile.DataList.Dequeue();
            //    handledata.UnitWashData(item);
            //    Console.WriteLine("  thi lan thu " + i + " /count " + importFile.DataList.Count);
            //    if (i % 3 == 0)
            //    {
            //        int timeSleep = 3000;
            //        Console.WriteLine("ngu khoang " + timeSleep / 1000);
            //        Thread.Sleep(timeSleep);
            //    }
            //}

            DataWash = importFile.DataList;

            var timer = new System.Timers.Timer(3000); // 1 seconds
            timer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
            timer.Interval = 3000;
            timer.Enabled = true;
            Console.WriteLine("Press the enter key to stop the timer");
            Console.ReadLine();
        }

        private static int k = 0;
        private static void OnTimerElapsed(object source, ElapsedEventArgs e)
        {

            if (DataWash == null)
            {
                return;
            }
            if (DataWash.Count < 1)
            {
                return;
            }
            var listRun = new List<WashDataItem>();

            var kidex = DataWash.Count > 5 ? 5 : DataWash.Count;


            for (int i = 0; i < kidex; i++)
            {
                var item = DataWash.Dequeue();
                listRun.Add(item);
            }
            //21218

            //    i++;
            //    var item = importFile.DataList.Dequeue();
            //    handledata.UnitWashData(item);
            //    Console.WriteLine("  thi lan thu " + i + " /count " + importFile.DataList.Count);
            //    if (i % 3 == 0)
            //    {
            //        int timeSleep = 3000;
            //        Console.WriteLine("ngu khoang " + timeSleep / 1000);
            //        Thread.Sleep(timeSleep);
            //    }


            Console.WriteLine("tong so phan tu con lai  at {0}", DataWash.Count);
            foreach (var itemrun in listRun)
            {
                k++;
                Console.WriteLine("Timer elapsed at {0}", DateTime.Now);
                Console.WriteLine("thuc thi  at {0}", k);
                handledata.UnitWashData(itemrun);
                Console.WriteLine("tong so phan tu con lai  at {0}", DataWash.Count);
            }


        }


    }
}
