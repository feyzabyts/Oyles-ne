using Microsoft.AspNetCore.Mvc;
using WebApplication5.Repositories;

namespace WebApplication5.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepo)
        {
            _userRepository = userRepo;
        }

        // Kayıt onay kısmı 

        [HttpPost("Approve")]
        public async Task<IActionResult> ApproveRegister([FromQuery] int userId, [FromQuery] int staffId)
        {
            var result = await _userRepository.ApproveRegisterAsync(userId, staffId);

            if (result)
            {
                return Ok(new { Message = "Approval successful" });
            }

            return BadRequest(new { Message = "Approval failed. User or staff not found, or staff is rejected." });
        }


        //mesaj gönderme
        [HttpPost("Send Message")]
        public async Task<IActionResult> SendMessage([FromQuery] int senderId, [FromQuery] int receiverId, [FromQuery] string content, [FromQuery] string message)
        {
            var result = await _userRepository.SendMessageAsync(senderId, receiverId, content,message);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }



        //Kullanıcı ceza

        [HttpPost("Punish User")]
        public async Task<IActionResult> Punish([FromQuery] int userId, [FromQuery] DateTime fDate, [FromQuery] DateTime lDate)
        {
            if (fDate > lDate)
            {
                return BadRequest(new { Message = "Başlangıç tarihi bitiş tarihinden büyük olamaz." });
            }

            var penalty = await _userRepository.PunishAsync(userId, fDate, lDate);

            if (penalty != 0)
            {
                return Ok(new { Penalty = penalty });
            }

            return BadRequest(new { Message = "Ceza hesaplanamadı veya geçersiz veri." });
        }




        //Görevli Ceza

        [HttpPost("punish")]
        public async Task<IActionResult> PunishStaff([FromQuery] int staffId, [FromQuery] DateTime date, [FromQuery] DateTime dateAns)
        {
            if (date > dateAns)
            {
                return BadRequest(new { Message = "Başlangıç tarihi bitiş tarihinden büyük olamaz." });
            }

            var penalty = await _userRepository.PunishStaffAsync(staffId, date, dateAns);

            if (penalty != 0)
            {
                return Ok(new { Penalty = penalty });
            }

            return BadRequest(new { Message = "Ceza hesaplanamadı veya geçersiz veri." });
        }


    }
}
