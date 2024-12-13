using DatabaseAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DatabaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AppDatabaseContext _databaseContext;

        public LoginController(AppDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // Login sorgusu için POST isteği
        [HttpPost("check")]
        public IActionResult CheckPersonel([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid request data" });
            }

            // Kullanıcının girdiği TCKN'nin son 3 hanesini alıyoruz
            var lastThreeDigits = request.TCKN.Substring(request.TCKN.Length - 3);

            // Veritabanında, girilen ad, soyad, e-posta ve TCKN'nin son 3 hanesini karşılaştırıyoruz
            var personel = _databaseContext.PersonelAccounts
                .FirstOrDefault(p => p.Name == request.Name
                                     && p.Surname == request.Surname
                                     && p.Email == request.Email
                                     && p.TCKN.Substring(p.TCKN.Length - 3) == lastThreeDigits);

            if (personel != null)
            {
                return Ok(new { Message = "Login Successful" });
            }
            else
            {
                return Unauthorized(new { Message = "Invalid Credentials" });
            }
        }

        // LoginRequest sınıfı, istenen giriş bilgilerini tutacak
        public class LoginRequest
        {
            public required string Name { get; set; }
            public required string Surname { get; set; }
            public required string TCKN { get; set; }
            public required string Email { get; set; }
        }
    }
}
