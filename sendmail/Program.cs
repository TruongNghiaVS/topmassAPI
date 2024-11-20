namespace sendmail
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var objectInfo = new SendMail();
            objectInfo.Send();
        }
    }
}
