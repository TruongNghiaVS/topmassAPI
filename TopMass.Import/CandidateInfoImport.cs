namespace TopMass.Import
{
    public class CandidateInfoImport
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CVLink { get; set; }
        public string CVLink2 { get; set; }
        protected string Postion { get; set; }
        public string PostionJob
        {
            get
            {
                if (string.IsNullOrEmpty(Postion))
                {
                    return string.Empty;
                }
                var arrayString = Postion.Split('_');
                if (arrayString.Length > 1)
                {
                    return arrayString[1];
                }
                return Postion;
            }
        }
    }
}
