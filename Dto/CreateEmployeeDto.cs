namespace AuthenticationAuthorization.Dto
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string NameInHindi { get; set; }
        public string Gender { get; set; }
        public string GenderInHindi { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CardNumber { get; set; }
        public IFormFile Photo { get; set; }
        public IFormFile QrCode { get; set; }
    }
}
