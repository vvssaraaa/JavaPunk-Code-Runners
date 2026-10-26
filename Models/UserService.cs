using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace Javapunk.Models
{

    public class UserService
    {

        private readonly Javapunk.Data.ApplicationDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(Javapunk.Data.ApplicationDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }
        //Creates a new user. Validates that the username isn't empty and is a reasonable length, then checks it isn't already taken. 
        //New users start with a score of 0.
        public async Task<Users?> CreateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 20)
            {
                return null;
            }
            try
            {
                if (await _context.Users.AnyAsync(u => u.User_name == username))
                {
                    return null;
                }
                Users user = new Users();
                user.User_name = username;
                user.User_score = 0;
                user.User_password = password;

                _context.Add(user);

                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create username, invalid input");
                return null;
            }
        }
        // Adds the score from a completed quiz run to the user's total score.
        public async Task<bool> AddScoreToUserScore(int userId, int score)
        {
            try
            {
                Users? user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    return false;
                }
                user.User_score += score;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to add score to user score");
                return false;
            }
        }

        public async Task<bool> MarkModuleComplete(int userId, int moduleId)
        {
            try
            {
                Users user = await _context.Users
                  .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new Exception("User not found");

                Modules module = await _context.Modules
                    .FirstOrDefaultAsync(m => m.Id == moduleId) ?? throw new Exception("Module not found");

                Completed_modules moduleComplete = new Completed_modules();
                moduleComplete.Users = user;
                moduleComplete.Modules = module;

                _context.Add(moduleComplete);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to mark module as complete");
                return false;
            }
        }

        public async Task<Completed_modules?> GetLastCompleted(int userId)
        {
            try
            {
                Completed_modules? lastCompletedModule = await _context.Completed_modules
                    .Where(cm => cm.Users.Id == userId)
                    .OrderByDescending(cm => cm.Id)
                    .FirstOrDefaultAsync();
                return lastCompletedModule;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to retrieve last completed module");
                return null;
            }
        }

        public async Task<Users?> GetUserWithUserName(String username)
        {
            try
            {
                Users? user = await _context.Users.FirstOrDefaultAsync(u => u.User_name == username);
                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to check if user exists");
                return null;
            }
        }
        public async Task<Users?> GetUserWithUserId(int userId)
        {
            try
            {
                Users? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to check if user exists");
                return null;
            }
        }
    }
}
