using AuthenticationAuthorization.Dto;
using AuthenticationAuthorization.Models;
using AuthenticationAuthorization.Repositories.Interfaces;
using CoreHtmlToImage;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAuthorization.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        private readonly IWebHostEnvironment _environment;
        public EmployeesController(IEmployeeRepository repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromForm]CreateEmployeeDto employeeDto)
        {
            if (employeeDto == null) return BadRequest();

            if (employeeDto.Photo.FileName == null || employeeDto.QrCode.FileName == null)
            {
                return BadRequest("File not selected");
            }

            
            var photoPath = Path.Combine(_environment.WebRootPath, "Images/", employeeDto.Photo.FileName);
            var qrcodePath = Path.Combine(_environment.WebRootPath, "Images/", employeeDto.QrCode.FileName);

            using (FileStream stream = new FileStream(photoPath, FileMode.Create))
            {
                await employeeDto.Photo.CopyToAsync(stream);
                stream.Close();
            }
            using (FileStream stream = new FileStream(qrcodePath, FileMode.Create))
            {
                await employeeDto.QrCode.CopyToAsync(stream);
                stream.Close();
            }

            var employee = new Employee
            {
                Name = employeeDto.Name,
                NameInHindi = employeeDto.NameInHindi,
                DateOfBirth = employeeDto.DateOfBirth,
                Gender = employeeDto.Gender,
                GenderInHindi = employeeDto.GenderInHindi,
                CardNumber = employeeDto.CardNumber,
                Photo = photoPath,
                QrCode = qrcodePath
            };

            try
            {
                await _repository.CreateEmployee(employee);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("/adhar-card/download")]
        public async Task<IActionResult> DownloadAdharCard([FromForm]string CardNumber)
        {
            if (CardNumber == null) return BadRequest("Card Number cannot be empty");

            try
            {
                Employee card = await _repository.GetCard(CardNumber);
                if (card == null) return BadRequest("Card not found");

                var backImage = Path.Combine(_environment.WebRootPath, "Images/", "adhar-card.png");

                var htmlContent = System.IO.File.ReadAllText("Static/adhar-template.html");

                htmlContent = htmlContent.Replace("{{BackgroundImage}}", backImage)
                                .Replace("{{Name}}", card.Name)
                                .Replace("{{NameInHindi}}", card.NameInHindi)
                                .Replace("{{DateOfBirth}}", card.DateOfBirth.ToString("dd/MM/yyyy"))
                                .Replace("{{Gender}}", card.Gender)
                                .Replace("{{GenderInHindi}}", card.GenderInHindi)
                                .Replace("{{CardNumber}}", card.CardNumber)
                                .Replace("{{Photo}}", card.Photo)
                                .Replace("{{QrCode}}", card.QrCode);
                var image = await GenerateImageAsync(htmlContent);

                return File(image, "image/png", $"Card_{card.CardNumber}.png");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [NonAction]
        private async Task<byte[]> GenerateImageAsync(string htmlContent)
        {
            var converter = new HtmlConverter();
            var bytes = converter.FromHtmlString(htmlContent, 700, ImageFormat.Png);
            return bytes;
        }

    }
}
