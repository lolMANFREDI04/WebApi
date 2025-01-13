using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.Entity;
using WebApi.Model.Request;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[AllowAnonymous]  // Questo permette l'accesso senza autenticazione
    public class ValuesController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        // Iniettare il servizio del database nel controller
        public ValuesController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // Endpoint protetto con autorizzazione
        //[Authorize]  // Applicato all'intero controller, quindi tutti gli endpoint richiedono autenticazione
        [HttpGet]
        public IActionResult GetUser(int IdUser)
        {
            var user = _databaseService.GetAllUsers().FirstOrDefault(x => x.IdUser == IdUser);

            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // Endpoint senza autorizzazione
        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult AllUsers()
        {
            return Ok(_databaseService.GetAllUsers());
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] AddUserRequest user)
        {
            var newUser = new User
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                Password = user.Password,
                Surname = user.Surname
            };

            _databaseService.AddUser(newUser);
            return Ok(newUser);
        }

        [HttpPut]
        public IActionResult UpdateUser(int idUser, [FromBody] UpdateUserRequest user)
        {
            var existingUser = _databaseService.GetAllUsers().FirstOrDefault(x => x.IdUser == idUser);

            if (existingUser == null)
                return NotFound();  // Se l'utente non esiste, restituisci 404

            // Aggiorna i dati dell'utente
            existingUser.UserName = user.UserName ?? existingUser.UserName;
            existingUser.FirstName = user.FirstName ?? existingUser.FirstName;
            existingUser.Password = user.Password ?? existingUser.Password;
            existingUser.Surname = user.Surname ?? existingUser.Surname;

            // Salva le modifiche nel database
            _databaseService.UpdateUser(existingUser);

            return Ok(existingUser);  // Restituisci l'utente aggiornato
        }

        [HttpDelete]
        public IActionResult DeleteUser(int idUser)
        {
            _databaseService.DeleteUser(idUser); // Chiama il metodo DeleteUser dal DatabaseService
            return Ok();
        }
    }
}
