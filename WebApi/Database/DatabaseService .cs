using WebApi.Entity;
using WebApi.Model;

namespace WebApi.Database
{
    public class DatabaseService
    {
        private readonly ApplicationDbContext _context;

        // Iniezione di dipendenza per il contesto del database
        public DatabaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Aggiungere un nuovo utente al database
        public void AddUser(User user)
        {
            _context.Users.Add(user); // Aggiungi l'utente al contesto
            _context.SaveChanges(); // Salva le modifiche nel database
        }

        // Recuperare tutti gli utenti dal database
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList(); // Ottieni la lista di utenti dal database
        }

        // Validare un utente tramite username e password
        public User ValidateUser(string username, string password)
        {
            return _context.Users.FirstOrDefault(user => user.UserName == username && user.Password == password);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user); // Aggiorna l'utente nel contesto
            _context.SaveChanges(); // Salva le modifiche nel database
        }

        // Metodo per eliminare un utente
        public void DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.IdUser == userId);
            if (user != null)
            {
                _context.Users.Remove(user); // Rimuovi l'utente dal contesto
                _context.SaveChanges(); // Salva le modifiche nel database
            }
        }
    }
}
