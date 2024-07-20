namespace AuthenticationAuthorization.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameInHindi { get; set; }
        public string Gender { get; set; }
        public string GenderInHindi { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CardNumber { get; set; }
        public string Photo {  get; set; }
        public string QrCode { get; set; }
    }
}
