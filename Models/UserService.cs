using Microsoft.EntityFrameworkCore;
namespace Javapunk.Models

public class UserService{

    private readonly Javapunk.Data.ApplicationDbContext _context;

    public UserService(Javapunk.Data.ApplicationDbContext context)
    {
        _context = context;
    }
    //laget en metode som lar spilleren lage en bruker, simple validering at input ikke er tomt og at brukernavnet ikke er for kort eller langt
    //hvis alt stemmer så lager den ny bruker, med userscore satt til 0
    public async Task<Users?> Create(string username){
            if(string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 20){
                return null;
            }
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
}