namespace TopMass.Import
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var objCandidate = new CandidateImportFromSource();
            objCandidate.HandleData();
        }
    }
}
