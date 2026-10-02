using Microsoft.EntityFrameworkCore;
namespace Javapunk.Models{

public class UserService{

    private readonly Javapunk.Data.ApplicationDbContext _context;
    private readonly ILogger<UserService> _logger;

    public UserService(Javapunk.Data.ApplicationDbContext context, ILogger <UserService> logger)
    {
        _context = context;
        _logger = logger;
    }
     //Creates a new user. Validates that the username isn't empty and is a reasonable length, then checks it isn't already taken. 
     //New users start with a score of 0.
    public async Task<Users?> CreateUser(string username){
            if(string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 20){
                return null;
            }
            try{
            if (await _context.Users.AnyAsync(u => u.User_name == username)){
                return null;
            }
                Users user = new Users();
                user.User_name = username;
                user.User_score = 0;

                _context.Add(user);

                await _context.SaveChangesAsync();
                
                return user; 
            }
            catch(Exception e){
                _logger.LogError(e, "Failed to create username, invalid input");
                return null;
            }
        }
        // Adds the score from a completed quiz run to the user's total score.
        public async Task<bool> AddScoreToUserScore(int userId, int score){
            try{
                Users? user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

                if(user == null)
                {
                    return false;
                }
                user.User_score += score;

                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception e){
                _logger.LogError(e, "Failed to add score to user score");
                return false;
            }
        }
    }
}
